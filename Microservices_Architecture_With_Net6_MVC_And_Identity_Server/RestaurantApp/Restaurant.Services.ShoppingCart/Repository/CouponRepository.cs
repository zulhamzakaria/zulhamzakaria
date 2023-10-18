// This is where the logic is

using Newtonsoft.Json;
using Restaurant.Services.ShoppingCart.Models.DTOs;

namespace Restaurant.Services.ShoppingCart.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient httpClient;

        // Calling the Coupon service so HTTPClient is needed
        public CouponRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CouponDTO> GetCoupon(string couponCode)
        {
            // Get from CouponAPI
            // Base address is set inside the Program file
            var response = await httpClient.GetAsync($"/api/coupon/{couponCode}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            if(resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(resp.Result));
            }
            // return new empty couponDTO
            return new CouponDTO();
        }
    }
}
