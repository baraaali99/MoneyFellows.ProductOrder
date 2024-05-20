namespace MoneyFellows.ProductOrder.Core.Models;

public class OrderDetail
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}