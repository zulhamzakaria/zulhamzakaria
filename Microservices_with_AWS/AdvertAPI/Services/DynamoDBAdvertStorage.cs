using AdvertAPI.Models;
using AdvertAPI.Models.Infrastructure;
using AdvertAPI.Models.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Services
{
    public class DynamoDBAdvertStorage : IAdvertStorageService
    {
        private readonly IMapper mapper;

        public DynamoDBAdvertStorage(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<string> Add(Advert model)
        {
            var dbModel = mapper.Map<AdvertDB>(model);

            dbModel.Id = new Guid().ToString();
            dbModel.CreatedDate = DateTime.UtcNow;
            dbModel.Status = AdvertStatus.Pending;

            using (var client = new AmazonDynamoDBClient())
            {
                using(var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync(dbModel);
                }
            }
            return dbModel.Id;
        }

        public async Task<bool> CheckHealthAsync()
        {
           using(var client = new AmazonDynamoDBClient())
            {
                var tableData = await client.DescribeTableAsync("DynamoDB_table_name");

                // returns 0 if false
                return string.Compare(tableData.Table.TableStatus, "active", true) == 0;
            }
        }

        public async Task Confirm(ConfirmAdvert model)
        {
           using(var client = new AmazonDynamoDBClient())
            {
                using(var context = new DynamoDBContext(client))
                {
                    var record = await context.LoadAsync<AdvertDB>(model.Id);
                    if (record == null)
                        throw new KeyNotFoundException($"no such user : {model.Id}");

                    if(model.Status == AdvertStatus.Active)
                    {
                        record.Status = AdvertStatus.Active;
                        await context.SaveAsync(record);
                        
                    }
                    else
                    {
                        await context.DeleteAsync(record);
                    }
                }
            }
        }

        public async Task<List<Advert>> GetAll()
        {
            using(var client = new AmazonDynamoDBClient())
            {
                using(var context = new DynamoDBContext(client))
                {
                    // for production, create index and run query on the index
                    // scan is expensive
                    var allItems = await context.ScanAsync<AdvertDB>(new List<ScanCondition>()).GetRemainingAsync();
                    return allItems.Select(item => mapper.Map<Advert>(item)).ToList();
                }
            }
        }
    }
}
