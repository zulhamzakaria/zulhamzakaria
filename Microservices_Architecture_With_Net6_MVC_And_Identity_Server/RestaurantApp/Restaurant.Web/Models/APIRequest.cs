using static Restaurant.Web.SD;

namespace Restaurant.Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string? URL { get; set; }
        public object? Data { get; set; }
        public string? Token { get; set; }
    }
}
