using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Shared.RequestFeatures;

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

    public async Task<PagedList<MealComponent>> GetMealComponentsAsync(Guid storeId, MealComponentParameters mealComponentParameters, bool trackChanges)
    {
        var mealComponents = await FindByCondition(m => m.StoreId.Equals(storeId), trackChanges)
        .OrderBy(m => m.Name)
        .Skip((mealComponentParameters.PageNumber-1)*mealComponentParameters.PageSize)
        .Take(mealComponentParameters.PageSize)
        .ToListAsync();

        var count = await FindByCondition(m => m.StoreId.Equals(storeId), trackChanges)
        .CountAsync();

        return new PagedList<MealComponent>(mealComponents, count, mealComponentParameters.PageNumber, mealComponentParameters.PageSize);
    }
}