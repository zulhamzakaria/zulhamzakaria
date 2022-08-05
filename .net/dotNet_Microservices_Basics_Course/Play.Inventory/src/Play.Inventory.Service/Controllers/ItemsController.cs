using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Common;
using Play.Inventory.Service.Clients;
using Play.Inventory.Service.DTOs;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> inventoryItemRepository;
        private readonly IRepository<CatalogueItem> catalogueItemRepository;
        public ItemsController(IRepository<InventoryItem> inventoryItemRepository, IRepository<CatalogueItem> catalogueItemRepository)
        {
            this.inventoryItemRepository = inventoryItemRepository;
            this.catalogueItemRepository = catalogueItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty) { return BadRequest(); }

            var inventoryItemEntities = await inventoryItemRepository.GetAllAsync(item => item.UserId == userId);

            var itemsId = inventoryItemEntities.Select(item => item.CatalogueItemId);
            var catalogueItemEntities = await catalogueItemRepository.GetAllAsync(item => itemsId.Contains(item.Id));

            var inventoryItemDTOs = inventoryItemEntities.Select(inventoryItem =>
            {
                var catalogueItem = catalogueItemEntities.Single(catalogueItem => catalogueItem.Id == inventoryItem.CatalogueItemId);
                return inventoryItem.AsDTO(catalogueItem.Name, catalogueItem.Description);
            });

            return Ok(inventoryItemDTOs
            );
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemsDTO grantItemsDTO)
        {
            var inventoryItem = await inventoryItemRepository.GetAsync(item => item.UserId == grantItemsDTO.userId && item.CatalogueItemId == grantItemsDTO.catalogueItemId);
            if (inventoryItem == null)
            {
                inventoryItem = new InventoryItem
                {
                    CatalogueItemId = grantItemsDTO.catalogueItemId,
                    UserId = grantItemsDTO.userId,
                    Quantity = grantItemsDTO.quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };

                await inventoryItemRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity = grantItemsDTO.quantity;
                await inventoryItemRepository.UpdateAsync(inventoryItem);
            }

            return Ok();
        }
    }
}