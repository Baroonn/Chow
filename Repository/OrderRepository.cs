using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateOrderForBuyer(Guid buyerId, Guid storeId, Order order)
    {
        order.BuyerId = buyerId;
        order.StoreId = storeId;
        order.Status = "unfulfilled";
        Create(order);
    }

    public async Task<IEnumerable<Order>> GetOrdersForBuyerAsync(Guid buyerId, bool trackChanges)
    {
        return await FindByCondition(o => o.BuyerId.Equals(buyerId), trackChanges)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersForStoreAsync(Guid storeId, bool trackChanges)
    {
        return await FindByCondition(o => o.StoreId.Equals(storeId), trackChanges)
            .ToListAsync();
    }

    public async Task<Order> GetSingleOrderForBuyerAsync(Guid buyerId, Guid orderId, bool trackChanges)
    {
        return await FindByCondition(o => o.BuyerId.Equals(buyerId) && o.Id.Equals(orderId), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<Order> GetSingleOrderForStoreAsync(Guid storeId, Guid orderId, bool trackChanges)
    {
        return await FindByCondition(o => o.StoreId.Equals(storeId) && o.Id.Equals(orderId), trackChanges)
            .SingleOrDefaultAsync();
    }
}