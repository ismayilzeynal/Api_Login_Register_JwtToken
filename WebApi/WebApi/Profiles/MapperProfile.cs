using AutoMapper;
using WebApi.Dtos.CategoryDtos;
using WebApi.Dtos.ProductDtos;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryReturnDto>()
                .ForMember(d => d.ImageUrl, map => map.MapFrom(src => "https://localhost:7197/img/" + src.ImageUrl))
                .ReverseMap();

            CreateMap<Category, CategoryInProductReturnDto>()
                           .ReverseMap();

            CreateMap<Product, ProductReturnDto>()
               .ForMember(d => d.Profit, map => map.MapFrom(src => src.SalePrice - src.CostPrice))
               .ReverseMap();
        }
    }

}
