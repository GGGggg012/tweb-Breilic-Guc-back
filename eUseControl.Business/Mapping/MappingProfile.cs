using AutoMapper;
using eUseControl.Domain.Entities;
using eUseControl.Model;

namespace eUseControl.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserData, UserView>();
            CreateMap<RegisterRequest, UserData>();
            CreateMap<Product, ProductView>()
                .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.IsAvailable()));
            CreateMap<ProductRequest, Product>();
            CreateMap<Order, OrderView>();
        }
    }
}
