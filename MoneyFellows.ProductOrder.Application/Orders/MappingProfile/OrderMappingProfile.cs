using AutoMapper;
using MoneyFellows.ProductOrder.Application.Orders.Commands;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Models;
using OrderDetails = MoneyFellows.ProductOrder.Core.Models.OrderDetails;

namespace MoneyFellows.ProductOrder.Application.Orders.MappingProfile;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, GetOrdersListQueryOutputDtoItem>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
            .ForMember(dest => dest.CustomerDetails, opt => opt.MapFrom(src => src.CustomerDetails));
        CreateMap<OrderDetails, GetOrderDetailsDto>();
        CreateMap<Order, GetOrderbyIdQueryDto>();

    }
}