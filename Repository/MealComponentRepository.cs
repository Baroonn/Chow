using Contracts;
using Entities.Models;
using Repository;

public class MealComponentRepository : RepositoryBase<MealComponent>, IMealComponentRepository
{
    public MealComponentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateMealComponentForStore(Guid storeId, MealComponent mealComponent)
    {
        mealComponent.StoreId = storeId;
        Create(mealComponent);
    }

    public void DeleteMealComponent(MealComponent mealComponent)
    {
        Delete(mealComponent);
    }

    public MealComponent GetMealComponent(Guid storeId, Guid mealComponentId, bool trackChanges)
    {
        return FindByCondition(m => m.StoreId.Equals(storeId) && m.Id.Equals(mealComponentId), trackChanges)
        .SingleOrDefault();
    }

    public IEnumerable<MealComponent> GetMealComponents(Guid storeId, bool trackChanges)
    {
        return FindByCondition(m => m.StoreId.Equals(storeId), trackChanges)
        .OrderBy(m => m.Name).ToList();
    }
}