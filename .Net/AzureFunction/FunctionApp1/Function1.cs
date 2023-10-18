using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp1.Models;
using FunctionApp1.Repository;
using Math.Functions;

namespace FunctionApp1
{
    public class Function1 : ISum, ISubstract
    {
        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {


            ClassMath classMath = new ClassMath();
            classMath.SumFromMathClass(1, 2);


            Detail detail = new Detail() { Id = 1, Name = "XYZ" };

            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        public int Substract(int x, int y)
        {
            return x - y;
        }

        public int Sum(int x, int y)
        {
            return x + y;
        }
    }

    public interface ISubstract
    {
        public int Substract(int x, int y);
    }

}
