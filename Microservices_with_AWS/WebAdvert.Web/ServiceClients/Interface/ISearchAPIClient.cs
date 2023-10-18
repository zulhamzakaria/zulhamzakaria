using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Messages;

namespace WebAdvert.Web.ServiceClients.Interface
{
    public interface ISearchAPIClient
    {
        Task<List<AdvertType>> Search(string keyword);
    }
}
