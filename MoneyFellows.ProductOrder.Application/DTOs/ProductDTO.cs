namespace MoneyFellows.ProductOrder.Application.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public byte[] ProductImage { get; set; }
    public decimal Price { get; set; }
    public string Merchant { get; set; }
}