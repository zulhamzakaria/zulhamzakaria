using BlazorAspNetCoreHostedDemo.Server.Services;
using BlazorAspNetCoreHostedDemo.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAspNetCoreHostedDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        [Route("GetProducts")]
        public ActionResult<List<Product>> GetProducts()
        {
            return productService.GetProducts();
        }

        [HttpPost]
        [Route("AddProduct")]
        public ActionResult AddProduct(Product product)
        {
            productService.AddProduct(product);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public ActionResult DeleteProduct([FromQuery]string productCode)
        {
            productService.DeleteProduct(productCode);
            return Ok();
        }
    }
}
