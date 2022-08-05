using System.Threading.Tasks;
using MassTransit;
using Play.Catalogue.Contracts;
using Play.Common;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogueItemUpdatedConsumer : IConsumer<CatalogueItemUpdated>
    {
        private readonly IRepository<CatalogueItem> repository;
        public CatalogueItemUpdatedConsumer(IRepository<CatalogueItem> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogueItemUpdated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.itemId);

            if (item == null)
            {
                item = new CatalogueItem
                {
                    Id = message.itemId,
                    Name = message.name,
                    Description = message.description
                };
                await repository.CreateAsync(item);
            }
            else
            {
                item.Name = message.name;
                item.Description = message.description;

                await repository.UpdateAsync(item);
            }


        }
    }
}