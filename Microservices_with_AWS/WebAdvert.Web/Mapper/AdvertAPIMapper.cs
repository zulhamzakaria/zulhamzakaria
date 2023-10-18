using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Models.Requests;
using WebAdvert.Web.Models.Response;

namespace WebAdvert.Web.Mapper
{
    public class AdvertAPIMapper : Profile
    {
        public AdvertAPIMapper()
        {
            CreateMap<AdvertAPI, CreateAdvert>().ReverseMap();
            CreateMap<CreateAdvertResponse, AdvertResponse>().ReverseMap();
            CreateMap<ConfirmAdvertRequest, ConfirmAdvert>().ReverseMap();
        }
    }
}
