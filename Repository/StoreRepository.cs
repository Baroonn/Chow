using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Extensions;
using Shared.RequestFeatures;

public class StoreRepository : RepositoryBase<Store>, IStoreRepository
{
    public StoreRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {

    }

    public void CreateStore(Store store)
    {
        Create(store);
    }

    public void DeleteStore(Store store)
    {
        Delete(store);
    }

    public async Task<PagedList<Store>> GetAllStoresAsync(StoreParameters storeParameters, bool trackChanges)
    {
        var stores = await FindAll(trackChanges)
        .OrderBy(s => s.Name)
        .Paginate(storeParameters.PageNumber, storeParameters.PageSize)
        .ToListAsync();

        var count = await FindAll(trackChanges).CountAsync();

        return new PagedList<Store>(stores, count, storeParameters.PageNumber, storeParameters.PageSize);
    }

    public async Task<Store> GetStoreAsync(Guid storeId, bool trackChanges)
    {
        return await FindByCondition(s => s.Id.Equals(storeId), trackChanges)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Store>> GetStoresByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        return await FindByCondition(s => ids.Contains(s.Id), trackChanges).ToListAsync();
    }
}