using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class OrderService : IOrderService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public OrderService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    private async Task<Buyer> GetBuyerAndCheckIfItExists(Guid buyerId, bool trackChanges)
    {
        var buyer = await _repository.Buyer.GetBuyerAsync(buyerId, trackChanges);
        if (buyer is null)
        {
            throw new BuyerNotFoundException(buyerId);
        }
        return buyer;
    }

    private async Task<Store> GetStoreAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var store = await _repository.Store.GetStoreAsync(id, trackChanges);
        if (store is null)
        {
            throw new StoreNotFoundException(id);
        }
        return store;
    }

    //private async Task<Order> GetOrderAndCheckIfItExists(Guid orderId, bool trackChanges)
    //{
    //    var order = await _repository.Order.GetOrderAsync(orderId, trackChanges);
    //    if (order is null)
    //    {
    //        throw new OrderNotFoundException(orderId);
    //    }
    //    return order;
    //}

    public async Task<OrderReadDto> CreateOrderForBuyerAsync(Guid buyerId, OrderCreateDto orderCreateDto, bool trackChanges)
    {
        var buyer = await GetBuyerAndCheckIfItExists(buyerId, trackChanges);

        if (orderCreateDto.StoreId == default)
        {
            throw new StoreNotFoundException(orderCreateDto.StoreId);
        }

        var store = await GetStoreAndCheckIfItExists(orderCreateDto.StoreId, trackChanges);

        var order = _mapper.Map<Order>(orderCreateDto);
        _repository.Order.CreateOrderForBuyer(buyerId, orderCreateDto.StoreId, order);
        await _repository.SaveAsync();

        var orderReadDto = _mapper.Map<OrderReadDto>(order);

        return orderReadDto;
    }

    public async Task<(IEnumerable<OrderReadDto> buyerOrders, PaginationMetaData metaData)> GetAllOrdersForBuyerAsync(Guid buyerId, OrderParameters orderParameters, bool trackChanges)
    {
        var buyer = await GetBuyerAndCheckIfItExists(buyerId, trackChanges);
        var orders = await _repository.Order.GetOrdersForBuyerAsync(buyerId, orderParameters, trackChanges);
        var ordersReadDto = _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        return (buyerOrders: ordersReadDto, metaData: orders.PaginationMetaData);
    }

    public async Task<(IEnumerable<OrderReadDto> storeOrders, PaginationMetaData metaData)> GetAllOrdersForStoreAsync(Guid storeId, OrderParameters orderParameters, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);
        var orders = await _repository.Order.GetOrdersForStoreAsync(storeId, orderParameters, trackChanges);
        var ordersReadDto = _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        return (storeOrders: ordersReadDto, metaData: orders.PaginationMetaData);
    }

    public async Task<OrderReadDto> GetOrderForBuyerAsync(Guid buyerId, Guid orderId, bool trackChanges)
    {
        var buyer = await GetBuyerAndCheckIfItExists(buyerId, trackChanges);

        var order = await _repository.Order.GetSingleOrderForBuyerAsync(buyerId, orderId, trackChanges);
        if (order is null)
        {
            throw new OrderNotFoundException(orderId);
        }

        var orderReadDto = _mapper.Map<OrderReadDto>(order);
        return orderReadDto;
    }

    public async Task<OrderReadDto> GetOrderForStoreAsync(Guid storeId, Guid orderId, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);

        var order = await _repository.Order.GetSingleOrderForStoreAsync(storeId, orderId, trackChanges);
        if (order is null)
        {
            throw new OrderNotFoundException(orderId);
        }

        var orderReadDto = _mapper.Map<OrderReadDto>(order);
        return orderReadDto;
    }
}