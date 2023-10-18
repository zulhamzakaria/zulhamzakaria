using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.ProductAPI.Models;
using Restaurant.Services.ProductAPI.Models.DTOs;
using Restaurant.Services.ProductAPI.Repository;

namespace Restaurant.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    //[ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        protected ResponseDTO responseDTO;

        public ProductAPIController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            //this.responseDTO = new ResponseDTO();
            this.responseDTO = new();
        }
        // remove Authorize to allow for Anonymous access
        [HttpGet]
        //[Authorize]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDTO> productDTOs = await productRepository.GetProducts();
                responseDTO.Result = productDTOs;

            }
            catch (Exception ex)
            {
                responseDTO.IsSuccess = false;
                responseDTO.ErrorMessages = new List<string> { ex.Message };
            }

            return responseDTO;

        }

        // remove Authorize to allow for Anonymous access
        [HttpGet]
        //[Authorize]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDTO productDTO = await productRepository.GetProductById(id);
                responseDTO.Result = productDTO;

            }
            catch (Exception ex)
            {
                responseDTO.IsSuccess = false;
                responseDTO.ErrorMessages = new List<string> { ex.Message };
            }

            return responseDTO;

        }

        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] ProductDTO productDTO)
        {
            try
            {
                ProductDTO model = await productRepository.CreateUpdateProduct(productDTO);
                responseDTO.Result = model;
            }
            catch (Exception ex)
            {
                responseDTO.IsSuccess = false;
                responseDTO.ErrorMessages = new List<string> { ex.Message };
            }
            return responseDTO;
        }


        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] ProductDTO productDTO)
        {
            try
            {
                ProductDTO model = await productRepository.CreateUpdateProduct(productDTO);
                responseDTO.Result = model;
            }
            catch (Exception ex)
            {
                responseDTO.IsSuccess = false;
                responseDTO.ErrorMessages = new List<string> { ex.Message };
            }
            return responseDTO;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ResponseDTO> Delete(int id)
        {
            try
            {
                Boolean isSuccess = await productRepository.DeleteProduct(id);
                responseDTO.Result = isSuccess;
            }
            catch (Exception ex)
            {
                responseDTO.IsSuccess = false;
                responseDTO.ErrorMessages = new List<string> { ex.Message };
            }
            return responseDTO;
        }
    }
}
