using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record StoreManipulationDto
{
    [Required(ErrorMessage="Store name is a required field")]
    [MaxLength(60, ErrorMessage = "Store name cannot be more than 40 characters")]
    public string? Name{ get; init; }
    [Required(ErrorMessage="Store Email is a required field")]
    public string? Email { get; init; }
    [Required(ErrorMessage="Store location is a required field")]
    public string? Location { get; init; }
    [Required(ErrorMessage="Store phone is a required field")]
    public string? Phone { get; init; }
    public IEnumerable<MealComponentCreateDto>? MealComponents;
}