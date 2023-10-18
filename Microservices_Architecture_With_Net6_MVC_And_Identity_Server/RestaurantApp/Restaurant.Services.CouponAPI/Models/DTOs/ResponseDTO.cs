namespace Restaurant.Services.CouponAPI.Models.DTOs
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; } = true;
        // Result maybe of any types like boolean, IEnumerable, Product model
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; } 
    }
}
