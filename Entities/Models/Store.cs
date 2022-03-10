using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Store
{
    [Column("StoreId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage="Store name is a required field")]
    [MaxLength(60, ErrorMessage = "Store name cannot be more than 40 characters")]
    public string? Name{ get; set; }
    [Required(ErrorMessage="Store Email is a required field")]
    public string? Email { get; set; }
    [Required(ErrorMessage="Store location is a required field")]
    public string? Location{ get; set; }
    [Required(ErrorMessage="Store phone is a required field")]
    public string? Phone{ get; set; }
    [DefaultValue(true)]
    public bool Is_Open{ get; set; }
    public ICollection<Order>? Orders{ get; set; }
    public ICollection<MealComponent>? MealComponents{ get; set; }
}