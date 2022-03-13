using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class MealComponentService : IMealComponentService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public MealComponentService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public MealComponentReadDto CreateMealComponentForStore(Guid storeId, MealComponentCreateDto mealComponentCreateDto, bool trackChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackChanges);
        if(store is null)
        {
            throw new StoreNotFoundException(storeId);
        }

        var mealComponent = _mapper.Map<MealComponent>(mealComponentCreateDto);
        _repository.MealComponent.CreateMealComponentForStore(storeId, mealComponent);
        _repository.Save();

        var mealComponentReadDto = _mapper.Map<MealComponentReadDto>(mealComponent);
        return mealComponentReadDto;
    }

    public void DeleteMealComponentForStore(Guid storeId, Guid id, bool trackChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackChanges);
        if (store is null)
        {
            throw new StoreNotFoundException(storeId);
        }

        var mealComponent = _repository.MealComponent.GetMealComponent(storeId, id, trackChanges);
        if (mealComponent is null)
        {
            throw new MealComponentNotFoundException(id);
        }

        _repository.MealComponent.DeleteMealComponent(mealComponent);
        _repository.Save();
    }

    public MealComponentReadDto GetMealComponent(Guid storeId, Guid mealComponentId, bool trackChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackChanges);
        if(store is null)
        {
            throw new StoreNotFoundException(storeId);
        }
        var mealComponent = _repository.MealComponent.GetMealComponent(storeId, mealComponentId, trackChanges);
        if (mealComponent is null)
        {
            throw new MealComponentNotFoundException(mealComponentId);
        }
        var mealComponentReadDto = _mapper.Map<MealComponentReadDto>(mealComponent);
        return mealComponentReadDto;
    }

    public (MealComponentUpdateDto mealComponentToPatch, MealComponent mealComponent) GetMealComponentForPatch(Guid storeId, Guid id, bool trackStoreChanges, bool trackMealComponentChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackStoreChanges);
        if(store is null)
        {
            throw new StoreNotFoundException(storeId);
        }

        var mealComponent = _repository.MealComponent.GetMealComponent(storeId, id, trackMealComponentChanges);
        if (mealComponent is null)
        {
            throw new MealComponentNotFoundException(id);
        }

        var mealComponentToPatch = _mapper.Map<MealComponentUpdateDto>(mealComponent);
        return (mealComponentToPatch, mealComponent);
    }

    public IEnumerable<MealComponentReadDto> GetMealComponents(Guid storeId, bool trackChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackChanges);
        if(store is null)
        {
            throw new StoreNotFoundException(storeId);
        }
        var mealComponents = _repository.MealComponent.GetMealComponents(storeId, trackChanges);
        var mealComponentsReadDto = _mapper.Map<IEnumerable<MealComponentReadDto>>(mealComponents);
        return mealComponentsReadDto;
    }

    public void SaveChangesForPatch(MealComponentUpdateDto mealComponentToPatch, MealComponent mealComponent)
    {
        _mapper.Map(mealComponentToPatch, mealComponent);
        _repository.Save();
    }

    public void UpdateMealComponentForStore(Guid storeId, Guid id, MealComponentUpdateDto mealComponentUpdateDto, bool trackStoreChanges, bool trackMealComponentChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackStoreChanges);
        if(store is null)
        {
            throw new StoreNotFoundException(storeId);
        }

        var mealComponent = _repository.MealComponent.GetMealComponent(storeId, id, trackMealComponentChanges);
        if(mealComponent is null)
        {
            throw new MealComponentNotFoundException(id);
        }

        _mapper.Map(mealComponentUpdateDto, mealComponent);
        _repository.Save();
    }
}