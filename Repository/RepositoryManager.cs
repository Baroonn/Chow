using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IStoreRepository> _storeRepository;
    private readonly Lazy<IBuyerRepository> _buyerRepository;
    private readonly Lazy<IOrderRepository> _orderRepository;
    private readonly Lazy<IMealComponentRepository> _mealComponentRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _storeRepository = new Lazy<IStoreRepository>(() => new StoreRepository(repositoryContext));
        _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(repositoryContext));
        _buyerRepository = new Lazy<IBuyerRepository>(() => new BuyerRepository(repositoryContext));
        _mealComponentRepository = new Lazy<IMealComponentRepository>(() => new MealComponentRepository(repositoryContext));
    }


    public IStoreRepository Store => _storeRepository.Value;

    public IBuyerRepository Buyer => _buyerRepository.Value;

    public IOrderRepository Order => _orderRepository.Value;

    public IMealComponentRepository MealComponent => _mealComponentRepository.Value;

    public void Save()
    {
        _repositoryContext.SaveChanges();
    }
}