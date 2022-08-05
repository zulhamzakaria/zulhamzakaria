using System.Threading.Tasks;
using BlazorAPIClient.DTO;

namespace BlazorAPIClient.DataServices
{
    public interface ISpaceXDataService
    {
        Task<LaunchDTO[]> GetAllLaunches(); 
        
    }
}