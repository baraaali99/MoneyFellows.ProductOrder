using System.ComponentModel.DataAnnotations;

namespace MoneyFellows.ProductOrder.Application.DTOs;

public class OrderDetailsDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public OrderDTO Order { get; set; }
    public Guid ProductId { get; set; }
    public ProductDTO Product { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}