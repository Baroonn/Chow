using Contracts;
using Service.Contracts;
using Entities.Models;
using Shared.DataTransferObjects;
using AutoMapper;
using Entities.Exceptions;

namespace Service;

internal sealed class StoreService : IStoreService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public StoreService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<StoreReadDto> CreateStoreAsync(StoreCreateDto storeCreateDto)
    {
        var store = _mapper.Map<Store>(storeCreateDto);
        _repository.Store.CreateStore(store);
        await _repository.SaveAsync();

        var storeReadDto = _mapper.Map<StoreReadDto>(store);
        return storeReadDto;
    }

    public async Task<(IEnumerable<StoreReadDto> storeReadDtoCollection, string ids)> CreateStoreCollectionAsync(IEnumerable<StoreCreateDto> storeCreateDtoCollection)
    {
        if(storeCreateDtoCollection is null)
        {
            throw new StoreCollectionBadRequest();
        }

        var storeCollection = _mapper.Map<IEnumerable<Store>>(storeCreateDtoCollection);
        foreach (var store in storeCollection)
        {
            _repository.Store.CreateStore(store);
        }
        await _repository.SaveAsync();

        var storeReadDtoCollection = _mapper.Map<IEnumerable<StoreReadDto>>(storeCollection);
        var ids = string.Join(',', storeReadDtoCollection.Select(s => s.Id));

        return (storeReadDtoCollection: storeReadDtoCollection, ids: ids);
    }

    public async Task DeleteStoreAsync(Guid storeId, bool trackChanges)
    {
        var store = await _repository.Store.GetStoreAsync(storeId, trackChanges);
        if (store is null)
        {
            throw new StoreNotFoundException(storeId);
        }

        _repository.Store.DeleteStore(store);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<StoreReadDto>> GetAllStoresAsync(bool trackChanges)
    {
        
        var stores = await _repository.Store.GetAllStoresAsync(trackChanges);
        var storesReadDto = _mapper.Map<IEnumerable<StoreReadDto>>(stores);
        return storesReadDto;
    }
    public async Task<StoreReadDto> GetStoreAsync(Guid storeId, bool trackChanges)
    {
        var store = await _repository.Store.GetStoreAsync(storeId, trackChanges);
        if (store is null)
        {
            throw new StoreNotFoundException(storeId);
        }
        var storeReadDto = _mapper.Map<StoreReadDto>(store);
        return storeReadDto;
    }
    public async Task<IEnumerable<StoreReadDto>> GetStoresByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
        {
            throw new IdParametersBadRequestException();
        }

        var stores = await _repository.Store.GetStoresByIdsAsync(ids, trackChanges);
        if (ids.Count() != stores.Count())
        {
            throw new CollectionsByIdsBadRequestException();
        }
        var storesReadDto = _mapper.Map<IEnumerable<StoreReadDto>>(stores);
        return storesReadDto;
    }

    public async Task UpdateStoreAsync(Guid storeId, StoreUpdateDto storeUpdateDto, bool trackChanges)
    {
        var store = await _repository.Store.GetStoreAsync(storeId, trackChanges);
        if(store is null)
        {
            throw new StoreNotFoundException(storeId);
        }

        _mapper.Map(storeUpdateDto, store);
        await _repository.SaveAsync();
    }
}