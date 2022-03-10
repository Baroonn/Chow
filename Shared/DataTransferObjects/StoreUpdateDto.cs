namespace Shared.DataTransferObjects;

public record StoreUpdateDto(string Name, string Email, string Location, string Phone, bool Is_Open, IEnumerable<MealComponentCreateDto> MealComponent);