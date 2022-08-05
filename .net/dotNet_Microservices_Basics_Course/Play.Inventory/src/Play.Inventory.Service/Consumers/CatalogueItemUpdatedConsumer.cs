using System.Threading.Tasks;
using MassTransit;
using Play.Catalogue.Contracts;
using Play.Common;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogueItemCreatedConsumer : IConsumer<CatalogueItemCreated>
    {
        private readonly IRepository<CatalogueItem> repository;
        public CatalogueItemCreatedConsumer(IRepository<CatalogueItem> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogueItemCreated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.itemId);

            if (item != null)
            {
                return;
            }

            item = new CatalogueItem
            {
                Id = message.itemId,
                Name = message.name,
                Description = message.desrcription
            };

            await repository.CreateAsync(item);
        }
    }
}