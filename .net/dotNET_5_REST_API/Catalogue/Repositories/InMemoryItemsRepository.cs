using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalogue.Entities;

namespace Catalogue.Repositories
{
    public class InMemoryItemsRepository : IInMemoryItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Polearm", Price = 30, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Zweihander", Price = 28, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Rapier", Price = 20, CreatedDate = DateTimeOffset.UtcNow }
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}