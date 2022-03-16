using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;

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

    public async Task<IEnumerable<Store>> GetAllStoresAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
        .OrderBy(s => s.Name)
        .ToListAsync();
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