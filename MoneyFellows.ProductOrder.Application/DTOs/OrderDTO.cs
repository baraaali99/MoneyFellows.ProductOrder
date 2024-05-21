namespace MoneyFellows.ProductOrder.Application.DTOs;

public class OrderDTO
{
    public Guid Id { get; set; }
    public string DeliveryAddress { get; set; }
    public decimal TotalCost { get; set; }
    public List<OrderDetailsDTO> OrderDetails { get; set; }
    public string CustomerDetails { get; set; }
    public DateTime DeliveryTime { get; set; }
}