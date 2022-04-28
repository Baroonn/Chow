using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Shared.RequestFeatures;

public class BuyerRepository : RepositoryBase<Buyer>, IBuyerRepository
{
    public BuyerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateBuyer(Buyer buyer)
    {
        Create(buyer);
    }

    public void DeleteBuyer(Buyer buyer)
    {
        Delete(buyer);
    }

    public async Task<IEnumerable<Buyer>> GetAllBuyersAsync(BuyerParameters buyerParameters, bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderBy(b => b.Name)
            .Skip((buyerParameters.PageNumber - 1) * buyerParameters.PageSize)
            .Take(buyerParameters.PageSize)
            .ToListAsync();
    }

    public async Task<Buyer> GetBuyerAsync(Guid buyerId, bool trackChanges)
    {
        return await FindByCondition(b => b.Id.Equals(buyerId), trackChanges).SingleOrDefaultAsync();
    }
}