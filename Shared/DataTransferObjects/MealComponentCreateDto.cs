using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record MealComponentCreateDto
{
    [Required(ErrorMessage="Meal Component Name is required")]
    public string? Name { get; init; }
    [Required(ErrorMessage="Meal Component Type is required")]

    public string? Type { get; init; }
    [Required(ErrorMessage="Meal Component Price is required")]
    public int Price { get; init; }
}