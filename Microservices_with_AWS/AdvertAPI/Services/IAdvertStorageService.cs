using AdvertAPI.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvertAPI.Services
{
    public interface IAdvertStorageService
    {
        Task<string> Add(Advert model);
        //Task<bool> Confirm(ConfirmAdvert model);
        Task Confirm(ConfirmAdvert model);
        Task<bool> CheckHealthAsync();
        Task<List<Advert>> GetAll();
    }
}
