using AutoMapper;
using MoneyFellows.ProductOrder.Application.Orders.Commands.CreateOrderCommand;
using MoneyFellows.ProductOrder.Application.Orders.Commands.UpdateOrderCommand;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Models;
using OrderDetail = MoneyFellows.ProductOrder.Core.Models.OrderDetail;

namespace MoneyFellows.ProductOrder.Application.Orders.MappingProfile;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<CreateOrderCommandCustomer, Customer>();
        CreateMap<CreateOrderCommandOrderDetail, OrderDetail>();

        CreateMap<UpdateOrderCommand, Order>();
        CreateMap<UpdateOrderCommandCustomer, Customer>();
        CreateMap<UpdateOrderCommandOrderDetail, OrderDetail>();

        CreateMap<Order, GetOrdersListQueryOutputDtoItem>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        CreateMap<OrderDetail, GetOrderDetailsDto>();
        CreateMap<Order, GetOrderbyIdQueryDto>();
        CreateMap<OrderDetail, OrderDetailByIdDto>();
        CreateMap<Customer, CustomerDetailsDto>();
    }
}