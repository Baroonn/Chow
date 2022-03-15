using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record StoreUpdateDto : StoreManipulationDto
{
    [Required(ErrorMessage="Specify if the store is open")]
    public bool Is_Open { get; init; }
}