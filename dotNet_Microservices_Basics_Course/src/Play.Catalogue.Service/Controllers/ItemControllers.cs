using System;
using Microsoft.AspNetCore.Mvc;
using Play.Catalogue.Service.DTOS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Play.Catalogue.Service.Entities;
using Play.Common;
using MassTransit;
using Play.Catalogue.Contracts;

namespace Play.Catalogue.Service.Controllers
{
    //hostname/items
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        private readonly IRepository<Item> itemsRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public ItemsController(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint)
        {
            this.itemsRepository = itemsRepository;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetAsync()
        {
            var item = (await itemsRepository.GetAllAsync()).Select(item => item.AsDTO());
            return item;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetByIdAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null) { return NotFound(); }

            return item.AsDTO();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> PostAsync(CreateItemDTO createItemDTO)
        {
            var item = new Item
            {
                Name = createItemDTO.name,
                Description = createItemDTO.description,
                Price = createItemDTO.price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);
            await publishEndpoint.Publish(new CatalogueItemCreated(item.Id, item.Name, item.Description));
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateItemDTO updateItemDTO)
        {
            var existingItem = await itemsRepository.GetAsync(id);
            if (existingItem == null) { return NotFound(); }

            existingItem.Name = updateItemDTO.name;
            existingItem.Description = updateItemDTO.description;
            existingItem.Price = updateItemDTO.price;

            await itemsRepository.UpdateAsync(existingItem);
            await publishEndpoint.Publish(new CatalogueItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {

            var item = await itemsRepository.GetAsync(id);
            if (item == null) { return NotFound(); }

            await itemsRepository.RemoveAsync(item.Id);
            await publishEndpoint.Publish(new CatalogueItemDeleted(item.Id));
            return NoContent();
        }
    }
}