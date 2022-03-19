using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Extensions;
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
        .Search(mealComponentParameters.SearchTerm)
        .FilterByType(mealComponentParameters.Type)
        .OrderBy(m => m.Name)
        .Paginate(mealComponentParameters.PageNumber, mealComponentParameters.PageSize)
        .ToListAsync();

        var count = await FindByCondition(m => m.StoreId.Equals(storeId), trackChanges)
        .CountAsync();

        return new PagedList<MealComponent>(mealComponents, count, mealComponentParameters.PageNumber, mealComponentParameters.PageSize);
    }
}