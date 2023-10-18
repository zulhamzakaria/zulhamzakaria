using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Models.Response;

namespace WebAdvert.Web.ServiceClients.Interface
{
    public interface IAdvertAPIClient
    {
        Task<AdvertResponse> Create(AdvertAPI model);
        Task<bool> Confirm(ConfirmAdvert model);
        Task<List<Advertisement>> GetAllAsync();
    }
}
