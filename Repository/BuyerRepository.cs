using Contracts;
using Entities.Models;
using Repository;

public class BuyerRepository : RepositoryBase<Buyer>, IBuyerRepository
{
    public BuyerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}