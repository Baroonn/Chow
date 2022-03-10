namespace Shared.DataTransferObjects;

public record MealComponentUpdateDto(string Name, string Type, bool Is_Available, int Price);