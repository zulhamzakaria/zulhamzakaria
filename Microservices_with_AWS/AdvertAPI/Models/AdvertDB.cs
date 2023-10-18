/*
    For DB 
*/

using AdvertAPI.Models.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using System;

namespace AdvertAPI.Models
{
    [DynamoDBTable("dynamodb_table_name")]
    public class AdvertDB
    {
        // attribute is needed to store data into the DynamoDB
        // no attribute = no data stored
       
        [DynamoDBHashKey]
        public string Id { get; set; }
        [DynamoDBProperty]
        public string Title { get; set; }
        [DynamoDBProperty]
        public string Description { get; set; }
        [DynamoDBProperty] 
        public double Price { get; set; }
        [DynamoDBProperty]
        public DateTime CreatedDate { get; set; }
        [DynamoDBProperty]
        public AdvertStatus Status { get; set; }
    }
}
