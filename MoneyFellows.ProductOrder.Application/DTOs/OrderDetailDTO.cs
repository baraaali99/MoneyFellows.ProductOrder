namespace MoneyFellows.ProductOrder.Application.DTOs;

public class OrderDetailDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}