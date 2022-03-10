namespace Shared.DataTransferObjects;

public record StoreCreateDto(string Name, string Email, string Location, string Phone, IEnumerable<MealComponentCreateDto> MealComponents);