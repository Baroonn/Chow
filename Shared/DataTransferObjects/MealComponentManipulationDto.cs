using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record MealComponentManipulationDto
{
    [Required(ErrorMessage="Meal Component Name is required")]
    public string? Name { get; init; }
    [Required(ErrorMessage="Meal Component Type is required")]
    public string? Type { get; init; }
    [Range(10, int.MaxValue, ErrorMessage =("Price cannot be less than 10 naira"))]
    public int Price { get; init; }
}