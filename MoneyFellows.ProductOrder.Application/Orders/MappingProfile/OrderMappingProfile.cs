using AutoMapper;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Orders.MappingProfile;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, GetOrdersListQueryOutputDtoItem>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        CreateMap<OrderDetails, OrderDetailDto>();
        CreateMap<Order, GetOrderbyIdQueryDto>();

    }
}