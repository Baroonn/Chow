using Contracts;
using Entities.Models;
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

    public IEnumerable<Store> GetAllStores(bool trackChanges)
    {
        return FindAll(trackChanges)
        .OrderBy(s => s.Name)
        .ToList();
    }

    public Store GetStore(Guid storeId, bool trackChanges)
    {
        return FindByCondition(s => s.Id.Equals(storeId), trackChanges)
        .SingleOrDefault();
    }

    public IEnumerable<Store> GetStoresByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        return FindByCondition(s => ids.Contains(s.Id), trackChanges).ToList();
    }
}