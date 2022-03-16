using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

    public async Task<MealComponent> GetMealComponentAsync(Guid storeId, Guid mealComponentId, bool trackChanges)
    {
        return await FindByCondition(m => m.StoreId.Equals(storeId) && m.Id.Equals(mealComponentId), trackChanges)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MealComponent>> GetMealComponentsAsync(Guid storeId, bool trackChanges)
    {
        return await FindByCondition(m => m.StoreId.Equals(storeId), trackChanges)
        .OrderBy(m => m.Name).ToListAsync();
    }
}