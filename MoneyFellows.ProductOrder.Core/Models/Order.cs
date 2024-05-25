using System.ComponentModel.DataAnnotations;

namespace MoneyFellows.ProductOrder.Core.Models;

public class Order
{   
    public Guid Id { get; set; }
    
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
    public string DeliveryAddress { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal TotalCost { get; set; }
    
    [Required]
    public List<OrderDetails> OrderDetails { get; set; }
    
    [Required]
    public CustomerDetails CustomerDetails { get; set; }
    
    [Required]
    public DateTime DeliveryTime { get; set; }
}