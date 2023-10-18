using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Nest;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebAdvert.Search.Worker.Helpers;
using WebAdvert.Search.Worker.Messages;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace WebAdvert.Search.Worker
{
    public class SearchWorkerFunction
    {
        private readonly IElasticClient client;

        public SearchWorkerFunction(IElasticClient client)
        {
            this.client = client;
        }

        // AWS call default consructor everytime it runs lambda function
        // this constructor will call SearchWorkerFunction(IElasticClient client) and allows for the singleton instance 
        // created by ElasticSearchHelper to be used
        public SearchWorkerFunction():this(ElasticSearchHelper.GetInstance(ConfigurationHelper.Instance))
        {

        }
        public async Task Function(SNSEvent snsEvent, ILambdaContext lambdaContext)
        {
            foreach(var record in snsEvent.Records)
            {
                lambdaContext.Logger.LogLine(record.Sns.Message);

                var message = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
                var advertDocument = MappingHelper.Map(message);

                await client.IndexDocumentAsync(advertDocument);

            }
        }
    }
}
