using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IBuyerService> _buyerService;
    private readonly Lazy<IOrderService> _orderService;
    private readonly Lazy<IMealComponentService> _mealComponentService;
    private readonly Lazy<IStoreService> _storeService;
    private readonly Lazy<IAuthenticationService> _authenticationService;

    public ServiceManager(IRepositoryManager repositoryManager, 
        ILoggerManager logger, 
        IMapper mapper,
        UserManager<User> userManager,
        IConfiguration configuration)
    {
        _buyerService = new Lazy<IBuyerService>(() => new BuyerService(repositoryManager, logger, mapper));
        _mealComponentService = new Lazy<IMealComponentService>(() => new MealComponentService(repositoryManager, logger, mapper));
        _orderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, logger, mapper));
        _storeService = new Lazy<IStoreService>(() => new StoreService(repositoryManager, logger, mapper));
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
    }

    public IBuyerService BuyerService => _buyerService.Value;

    public IMealComponentService MealComponentService => _mealComponentService.Value;

    public IOrderService OrderService => _orderService.Value;

    public IStoreService StoreService => _storeService.Value;

    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}