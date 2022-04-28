using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Shared.RequestFeatures;

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

    public async Task<PagedList<Order>> GetOrdersForBuyerAsync(Guid buyerId, OrderParameters orderParameters, bool trackChanges)
    {
        var buyerOrders = await FindByCondition(o => o.BuyerId.Equals(buyerId), trackChanges)
            .OrderBy(o => o.BuyerId)
            .Skip((orderParameters.PageNumber-1)*orderParameters.PageSize)
            .Take(orderParameters.PageSize)
            .ToListAsync();

        var count = await FindByCondition(o => o.BuyerId.Equals(buyerId), trackChanges).CountAsync();

        return new PagedList<Order>(buyerOrders, count, orderParameters.PageNumber, orderParameters.PageSize);
    }

    public async Task<PagedList<Order>> GetOrdersForStoreAsync(Guid storeId, OrderParameters orderParameters, bool trackChanges)
    {
        var storeOrders = await FindByCondition(o => o.StoreId.Equals(storeId), trackChanges)
            .OrderBy(o => o.BuyerId)
            .Skip((orderParameters.PageNumber - 1) * orderParameters.PageSize)
            .Take(orderParameters.PageSize)
            .ToListAsync();

        var count = await FindByCondition(o => o.StoreId.Equals(storeId), trackChanges).CountAsync();

        return new PagedList<Order>(storeOrders, count, orderParameters.PageNumber, orderParameters.PageSize);
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