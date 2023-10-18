using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Models.AdvertManagement
{
    public class Advertisement
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string FilePath { get; set; }
    }
}
