using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Order
{
    [Column("OrderId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage="Description is required")]
    public string? Description{ get; set; }
    [Required(ErrorMessage="Status is required")]
    public string? Status { get; set; }
    [Required(ErrorMessage="BuyerPhone is required")]
    public string? BuyerPhone { get; set; }
    [Required(ErrorMessage="BuyerLocation is required")]
    public string? BuyerLocation{ get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [ForeignKey(nameof(Store))]
    public Guid StoreId{ get; set; }
    public Store? Store { get; set; }
    [ForeignKey(nameof(Buyer))]
    public Guid BuyerId{ get; set; }
    public Buyer? Buyer { get; set; }
}