using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Infrastructure;

namespace WebAdvert.Web.Models.AdvertManagement
{
    public class ConfirmAdvert
    {
        public string Id { get; set; }
        public AdvertStatus Status { get; set; }
        public string FilePath { get; set; }
    }
}
