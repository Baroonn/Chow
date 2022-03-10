namespace Shared.DataTransferObjects;

public record StoreReadDto(Guid Id, string Name, string Location, string Phone, string Is_Open);