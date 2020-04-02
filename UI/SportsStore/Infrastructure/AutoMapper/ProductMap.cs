using AutoMapper;
using SportsStore.Domain.Models;

namespace SportsStore.Infrastructure.AutoMapper
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Product, Product>()
               .ForMember(p => p.Id, id => id.Ignore());
        }
    }
}
