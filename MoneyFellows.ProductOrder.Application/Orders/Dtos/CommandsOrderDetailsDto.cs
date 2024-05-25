namespace MoneyFellows.ProductOrder.Application.Orders.Dtos;

public class CommandsOrderDetailsDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}