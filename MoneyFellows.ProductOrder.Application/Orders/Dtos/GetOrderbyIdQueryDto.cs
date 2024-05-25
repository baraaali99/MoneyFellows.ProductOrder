using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Orders.Dtos;

public class GetOrderbyIdQueryDto
{
    public int Id { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public List<OrderDetailByIdDto> OrderDetails { get; set; }
    public CustomerDetailsDto Customer { get; set; }
    public DateTime DeliveryTime { get; set; }   
}
public class OrderDetailByIdDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}