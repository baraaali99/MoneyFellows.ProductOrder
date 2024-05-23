namespace MoneyFellows.ProductOrder.Application.Orders.Dtos;

public class GetOrderbyIdQueryDto
{
    public Guid Id { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public List<OrderDetailByIdDto> OrderDetails { get; set; }
    public string CustomerDetails { get; set; } = string.Empty;
    public DateTime DeliveryTime { get; set; }   
}
public class OrderDetailByIdDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}