using System.ComponentModel.DataAnnotations;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.DTOs;

public class OrderDetailsDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid ProductId { get; set; }
    public  Product product { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}