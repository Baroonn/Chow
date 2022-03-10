namespace Entities.Exceptions;

public class CollectionsByIdsBadRequestException : Exception
{
    public CollectionsByIdsBadRequestException()
    :base("Collection count mismatch comparing to ids")
    {

    }
}