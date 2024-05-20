namespace MoneyFellows.ProductOrder.Core.Models;

public class Product
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public byte[] ProductImage { get; set; }
    public decimal Price { get; set; }
    public string Merchant { get; set; }
}
