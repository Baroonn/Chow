namespace Entities.Exceptions;

public class MealComponentNotFoundException : NotFoundException
{
    public MealComponentNotFoundException(Guid mealComponentId)
    :base($"Meal Component with id: {mealComponentId} does not exist in the database.")
    {

    }
}