using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyFellows.ProductOrder.Core.Models;

public class OrderDetail
{
    public int Id { get; set; }
    
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    
    public Order Order { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    
    public Product Product { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}