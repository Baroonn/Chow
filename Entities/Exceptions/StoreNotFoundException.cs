namespace Entities.Exceptions;

public sealed class StoreNotFoundException : NotFoundException
{
    public StoreNotFoundException(Guid storeId)
        :base($"The store with the id: {storeId} doesn't exist in the database.")
    {

    }
}