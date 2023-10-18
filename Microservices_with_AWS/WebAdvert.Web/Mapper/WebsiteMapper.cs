using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Models.ViewModel;

namespace WebAdvert.Web.Mapper
{
    public class WebsiteMapper : Profile
    {
        public WebsiteMapper()
        {
            CreateMap<CreateAdvert, AdvertAPI>().ReverseMap();

            CreateMap<AdvertAPI, Advertisement>().ReverseMap();

            CreateMap<Advertisement, IndexViewModel>()
                .ForMember(destination =>
                                destination.Title, src => src.MapFrom(field => field.Title))
                .ForMember(destination =>
                                destination.Image, src => src.MapFrom(field => field.FilePath));

        }
    }
}
