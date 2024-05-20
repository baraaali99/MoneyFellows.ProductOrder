using System.ComponentModel.DataAnnotations;

namespace MoneyFellows.ProductOrder.Core.Models;

public class OrderDetail
{
    public Guid ProductId { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}