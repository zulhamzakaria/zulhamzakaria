using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ProductAPI.AppDbContext;
using Restaurant.Services.ProductAPI.Models;
using System.Collections.Generic;

namespace Restaurant.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                Product produt = await context.Products.FindAsync(id);
                if(produt == null)
                {
                    return false;
                }
                context.Products.Remove(produt);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            Product product = await context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            return mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            /*
                assign data gotten from db to the model
                then convert the result to dto
            */
            IEnumerable<Product> products = await context.Products.ToListAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        // Upsert
        public async Task<ProductDTO> CreateUpdateProduct(ProductDTO productDTO)
        {
            // convert to model first since the argument accepts DTO and 
            // db should work with model
            Product product = mapper.Map<ProductDTO,Product>(productDTO);

            if(product.ProductId > 0)
            {
                context.Products.Update(product);
            }
            else
            {
                context.Products.Add(product);
            }
            await context.SaveChangesAsync();

            // convert Product to ProductDTO using result
            return mapper.Map<Product,ProductDTO>(product);
        }
    }
}
