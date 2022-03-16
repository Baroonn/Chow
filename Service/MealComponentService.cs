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

    private async Task<Store> GetStoreAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var store = await _repository.Store.GetStoreAsync(id, trackChanges);
        if (store is null)
        {
            throw new StoreNotFoundException(id);
        }
        return store;
    }

    private async Task<MealComponent> GetMealComponentForStoreAndCheckIfItExists(Guid storeId, Guid id, bool trackChanges)
    {
        var mealComponent = await _repository.MealComponent.GetMealComponentAsync(storeId, id, trackChanges);
        if (mealComponent is null)
        {
            throw new MealComponentNotFoundException(id);
        }
        return mealComponent;
    }

    public async Task<MealComponentReadDto> CreateMealComponentForStoreAsync(Guid storeId, MealComponentCreateDto mealComponentCreateDto, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);

        var mealComponent = _mapper.Map<MealComponent>(mealComponentCreateDto);
        _repository.MealComponent.CreateMealComponentForStore(storeId, mealComponent);
        await _repository.SaveAsync();

        var mealComponentReadDto = _mapper.Map<MealComponentReadDto>(mealComponent);
        return mealComponentReadDto;
    }

    public async Task DeleteMealComponentForStoreAsync(Guid storeId, Guid id, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);

        var mealComponent = await GetMealComponentForStoreAndCheckIfItExists(storeId, id, trackChanges);
        _repository.MealComponent.DeleteMealComponent(mealComponent);
        await _repository.SaveAsync();
    }

    public async Task<MealComponentReadDto> GetMealComponentAsync(Guid storeId, Guid mealComponentId, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);

        var mealComponent = await GetMealComponentForStoreAndCheckIfItExists(storeId, mealComponentId, trackChanges);
        var mealComponentReadDto = _mapper.Map<MealComponentReadDto>(mealComponent);
        return mealComponentReadDto;
    }

    public async Task<(MealComponentUpdateDto mealComponentToPatch, MealComponent mealComponent)> GetMealComponentForPatchAsync(Guid storeId, Guid id, bool trackStoreChanges, bool trackMealComponentChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackStoreChanges);

        var mealComponent = await GetMealComponentForStoreAndCheckIfItExists(storeId, id, trackMealComponentChanges);

        var mealComponentToPatch = _mapper.Map<MealComponentUpdateDto>(mealComponent);
        return (mealComponentToPatch, mealComponent);
    }

    public async Task<IEnumerable<MealComponentReadDto>> GetMealComponentsAsync(Guid storeId, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);
        var mealComponents = await _repository.MealComponent.GetMealComponentsAsync(storeId, trackChanges);
        var mealComponentsReadDto = _mapper.Map<IEnumerable<MealComponentReadDto>>(mealComponents);
        return mealComponentsReadDto;
    }

    public async Task SaveChangesForPatchAsync(MealComponentUpdateDto mealComponentToPatch, MealComponent mealComponent)
    {
        _mapper.Map(mealComponentToPatch, mealComponent);
        await _repository.SaveAsync();
    }

    public async Task UpdateMealComponentForStoreAsync(Guid storeId, Guid id, MealComponentUpdateDto mealComponentUpdateDto, bool trackStoreChanges, bool trackMealComponentChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackStoreChanges);

        var mealComponent = await GetMealComponentForStoreAndCheckIfItExists(storeId, id, trackMealComponentChanges);

        _mapper.Map(mealComponentUpdateDto, mealComponent);
        await _repository.SaveAsync();
    }
}