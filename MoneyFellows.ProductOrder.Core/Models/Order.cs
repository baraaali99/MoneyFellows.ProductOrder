namespace MoneyFellows.ProductOrder.Core.Models;

public class Order
{   
    public Guid Id { get; set; }
    public string DeliveryAddress { get; set; }
    public decimal TotalCost { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
    public string CustomerDetails { get; set; }
    public DateTime DeliveryTime { get; set; }
}