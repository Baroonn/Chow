using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class MealComponent
{
    [Column("MealComponentId")]
    public Guid Id{ get; set; }
    [Required(ErrorMessage="Meal Component Name is required")]
    public string? Name{ get; set; }
    [Required(ErrorMessage="Meal Component Type is required")]
    public string? Type{ get; set; }
    [DefaultValue(true)]
    public bool Is_Available{ get; set; }
    [Required(ErrorMessage="Meal Component Price is required")]
    public int Price { get; set; }
    [ForeignKey(nameof(Store))]
    public Guid StoreId{ get; set; }
    public Store? Store { get; set; }
}