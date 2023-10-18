using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ShoppingCart.Infrastructure;
using Restaurant.Services.ShoppingCart.Models;
using Restaurant.Services.ShoppingCart.Models.DTOs;

namespace Restaurant.Services.ShoppingCart.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var cartHeader = await context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
            cartHeader.CouponCode = couponCode;
            context.CartHeaders.Update(cartHeader);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCart(string userID)
        {
            // Retrieve CartHeader from database
            var header = await context.CartHeaders.FirstOrDefaultAsync(h => h.UserId == userID);
            if (header != null)
            {
                // RemoverRange() since it may be more than one items
                context.CartDetails.RemoveRange(context.CartDetails.Where(d => d.CartHeaderId == header.CartHeaderId));

                context.CartHeaders.Remove(header);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartDTO> GetCartByUserId(string userID)
        {
            Cart cart = new()
            {
                CartHeader = await context.CartHeaders.FirstOrDefaultAsync(h => h.UserId == userID)
            };
            cart.CartDetails = context.CartDetails
                                        .Where(d => d.CartHeaderId == cart.CartHeader.CartHeaderId)
                                        .Include(p => p.Product);

            return mapper.Map<CartDTO>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var cartHeader = await context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
            cartHeader.CouponCode = String.Empty;
            context.CartHeaders.Update(cartHeader);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveProductFromCart(int cartDetailsId)
        {
            // Get CartDetails by id
            CartDetails cartDetails = await context.CartDetails.FirstOrDefaultAsync(d => d.CartDetailsId == cartDetailsId);
            int cartItemsTotalCount = context.CartDetails.Where(d => d.CartHeaderId == cartDetails.CartHeaderId).Count();

            context.CartDetails.Remove(cartDetails);

            if (cartItemsTotalCount == 1)
            {
                var cartHeaderToRemove = await context.CartHeaders.FirstOrDefaultAsync(h => h.CartHeaderId == cartDetails.CartHeaderId);
                context.CartHeaders.Remove(cartHeaderToRemove);
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<CartDTO> UpsertCart(CartDTO cartDTO)
        {
            // Convert cartDTO to cart object
            // Cart deals with database
            Cart cart = mapper.Map<Cart>(cartDTO);

            // Check Product existance
            var result = await context.Products.FirstOrDefaultAsync(u => u.ProductId == cartDTO.CartDetails.FirstOrDefault().ProductId);

            if (result == null)
            {
                context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await context.SaveChangesAsync();
            }

            // If Header is null, create Header and Details
            var header = await context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(h => h.UserId == cart.CartHeader.UserId);

            if (header == null)
            {
                context.CartHeaders.Add(cart.CartHeader);
                await context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;

                // Set the Product to be null so that the database wont add the Product for the second time
                cart.CartDetails.FirstOrDefault().Product = null;

                context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await context.SaveChangesAsync();
            }
            else
            {
                /*
                    Header is not null, 
                    If details has same product, then update the count. Else, create details
                */
                // AsNoTracking() -> EF wont track the result means no updating the object. Tis to avoid error 
                var cartDetails = await context.CartDetails.AsNoTracking()
                    .FirstOrDefaultAsync(d => d.ProductId == cart.CartDetails.FirstOrDefault().ProductId && d.CartHeaderId == header.CartHeaderId);

                if (cartDetails == null)
                {
                    // Populate CartHeaderId
                    cart.CartDetails.FirstOrDefault().CartHeaderId = header.CartHeaderId;
                    // Set product to Null
                    cart.CartDetails.FirstOrDefault().Product = null;
                    // Save to database
                    context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await context.SaveChangesAsync();
                }
                else
                {
                    // Set product to Null
                    cart.CartDetails.FirstOrDefault().Product = null;
                    // Increment then update Count
                    cart.CartDetails.FirstOrDefault().Count += cartDetails.Count;
                    cart.CartDetails.FirstOrDefault().CartDetailsId = cartDetails.CartDetailsId;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetails.CartHeaderId;
                    context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await context.SaveChangesAsync();
                }
            }
            return mapper.Map<CartDTO>(cart);
        }
    }
}
