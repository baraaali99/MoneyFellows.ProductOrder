namespace MoneyFellows.ProductOrder.Application.Orders.Dtos;

public class GetOrdersListQueryOutputDto
{
    public List<GetOrdersListQueryOutputDtoItem> Items { get; set; } = new List<GetOrdersListQueryOutputDtoItem>();
}
public class GetOrdersListQueryOutputDtoItem
{
    public int Id { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public List<GetOrderDetailsDto> OrderDetails { get; set; }
    public CustomerDetailsDto Customer { get; set; }
    public DateTime DeliveryTime { get; set; }
}

public class GetOrderDetailsDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}