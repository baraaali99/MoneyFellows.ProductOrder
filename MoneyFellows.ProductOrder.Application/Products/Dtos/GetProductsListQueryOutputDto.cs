namespace MoneyFellows.ProductOrder.Application.Products.Dtos;

public class GetProductsListQueryOutputDto
{
    public List<GetProductsListQueryOutputDtoItem> Items { get; set; } = 
        new List<GetProductsListQueryOutputDtoItem>();
    
}

public class GetProductsListQueryOutputDtoItem
{
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public byte[]? ProductImage { get; set; }
    public decimal Price { get; set; }
    public string Merchant { get; set; } = string.Empty;
}