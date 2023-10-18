using Restaurant.Web.Models;
using Restaurant.Web.Models.DTOs;

namespace Restaurant.Web.Services.IServices
{
    public interface IBaseService:IDisposable
    {
        ResponseDTO responseDTO { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);

    }
}
