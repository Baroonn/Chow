namespace Shared.DataTransferObjects;

public record MealComponentReadDto(Guid Id, string Name, string Type, bool Is_Available, int Price);