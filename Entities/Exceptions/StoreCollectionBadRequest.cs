namespace Entities.Exceptions;

public class StoreCollectionBadRequest : BadRequestException
{
    public StoreCollectionBadRequest()
    :base("Store collection from client is null")
    {
        
    }
}