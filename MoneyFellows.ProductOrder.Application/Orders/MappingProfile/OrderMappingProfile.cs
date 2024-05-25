using AutoMapper;
using MoneyFellows.ProductOrder.Application.Orders.Commands;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Models;
using OrderDetail = MoneyFellows.ProductOrder.Core.Models.OrderDetail;

namespace MoneyFellows.ProductOrder.Application.Orders.MappingProfile;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderCommand, Product>();
        CreateMap<Order, GetOrdersListQueryOutputDtoItem>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        CreateMap<OrderDetail, GetOrderDetailsDto>();
        CreateMap<Order, GetOrderbyIdQueryDto>();

    }
}