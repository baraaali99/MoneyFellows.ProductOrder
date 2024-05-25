using AutoMapper;
using MoneyFellows.ProductOrder.Application.Products.Commands;
using MoneyFellows.ProductOrder.Application.Products.Dtos;
using MoneyFellows.ProductOrder.Core.Models;


namespace MoneyFellows.ProductOrder.Application.Products.MappingProfile
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, GetProductsListQueryOutputDtoItem>().ReverseMap();
            CreateMap<Product, GetProductByIdQueryDto>();
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}