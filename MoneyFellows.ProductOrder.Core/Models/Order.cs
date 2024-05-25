using System.ComponentModel.DataAnnotations;

namespace MoneyFellows.ProductOrder.Core.Models;

public class Order
{   
    public int Id { get; set; }
    
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
    public string DeliveryAddress { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal TotalCost { get; set; }
    
    [Required]
    public List<OrderDetail> OrderDetails { get; set; }
    
    [Required]
    public Customer Customer { get; set; }

    [Required]
    public int CustomerId { get; set; }
    
    [Required]
    public DateTime DeliveryTime { get; set; }
}