using System.ComponentModel.DataAnnotations;

namespace MoneyFellows.ProductOrder.Core.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
    public string ProductName { get; set; }
    
    [Required]
    [StringLength(500)]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
    public string ProductDescription { get; set; }
    
    public byte[]? ProductImage { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    
    [Required]
    [StringLength(100)]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
    public string Merchant { get; set; }
}
