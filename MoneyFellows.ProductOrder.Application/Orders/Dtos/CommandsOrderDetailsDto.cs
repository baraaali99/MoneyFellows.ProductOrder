namespace MoneyFellows.ProductOrder.Application.Orders.Dtos;

public class CommandsOrderDetailsDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}