namespace MoneyFellows.ProductOrder.Application.Products.Dtos;

public class GetProductByIdQueryDto
{
    private string ProductName { get; set; } = string.Empty;
    private string ProductDescription { get; set; } = string.Empty;
    private byte[]? ProductImage { get; set; }
    private decimal Price { get; set; }
    private string Merchant { get; set; } = string.Empty;
}