using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record MealComponentUpdateDto : MealComponentManipulationDto
{
    [Required(ErrorMessage="Specify if the component is available")]
    public bool Is_Available { get; init; }
}