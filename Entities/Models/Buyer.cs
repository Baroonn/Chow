using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Buyer
{
    [Column("BuyerId")]
    public Guid Id{ get; set; }
    [Required(ErrorMessage="Buyer Name is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage="Buyer Email is required")]
    public string? Email { get; set; }
    [Required(ErrorMessage="Buyer Phone is required")]
    public string? Phone { get; set; }
    public ICollection<Order>? Orders { get; set; }
}