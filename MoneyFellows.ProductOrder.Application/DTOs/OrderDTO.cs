using System.ComponentModel.DataAnnotations;

namespace MoneyFellows.ProductOrder.Application.DTOs;

public class OrderDTO
{
    public Guid Id { get; set; }
    
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$")]
    public string DeliveryAddress { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal TotalCost { get; set; }
    
    [Required]
    public List<OrderDetailsDTO> OrderDetails { get; set; }
    
    [Required]
    [StringLength(500)]
    public string CustomerDetails { get; set; }
    
    [Required]
    public DateTime DeliveryTime { get; set; }
}