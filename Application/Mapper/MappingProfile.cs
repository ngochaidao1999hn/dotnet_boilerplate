using Application.Commands.ProductsCommands;
using Application.Dtos.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<AddProductDto, Product>();
            CreateMap<Product, ProductReadDto>();
        }
    }
}
