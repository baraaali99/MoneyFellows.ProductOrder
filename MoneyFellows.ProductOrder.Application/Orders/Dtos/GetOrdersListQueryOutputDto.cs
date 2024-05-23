namespace MoneyFellows.ProductOrder.Application.Orders.Dtos;

public class GetOrdersListQueryOutputDto
{
    public List<GetOrdersListQueryOutputDtoItem> Items { get; set; } = new List<GetOrdersListQueryOutputDtoItem>();
}
public class GetOrdersListQueryOutputDtoItem
{
    public Guid Id { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
    public string CustomerDetails { get; set; } = string.Empty;
    public DateTime DeliveryTime { get; set; }
}

public class OrderDetailDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}