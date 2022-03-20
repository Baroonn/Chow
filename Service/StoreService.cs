using Contracts;
using Service.Contracts;
using Entities.Models;
using Shared.DataTransferObjects;
using AutoMapper;
using Entities.Exceptions;
using Shared.RequestFeatures;

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

    private async Task<Store> GetStoreAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var store = await _repository.Store.GetStoreAsync(id, trackChanges);
        if (store is null)
        {
            throw new StoreNotFoundException(id);
        }
        return store;
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
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);

        _repository.Store.DeleteStore(store);
        await _repository.SaveAsync();
    }

    public async Task<(IEnumerable<StoreReadDto> storeReadDtos, PaginationMetaData metaData)> GetAllStoresAsync(StoreParameters storeParameters, bool trackChanges)
    {
        
        var storesWithMetaData = await _repository.Store.GetAllStoresAsync(storeParameters, trackChanges);
        var storeReadDtos = _mapper.Map<IEnumerable<StoreReadDto>>(storesWithMetaData);
        return (storeReadDtos, metaData:storesWithMetaData.PaginationMetaData);
    }
    public async Task<StoreReadDto> GetStoreAsync(Guid storeId, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);
        var storeReadDto = _mapper.Map<StoreReadDto>(store);
        return storeReadDto;
    }

    public async Task<(StoreUpdateDto storeToPatch, Store store)> GetStoreForPatchAsync(Guid storeId, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);

        var storeToPatch = _mapper.Map<StoreUpdateDto>(store);
        return (storeToPatch, store);
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

    public async Task SaveChangesForPatchAsync(StoreUpdateDto storeToPatch, Store store)
    {
        _mapper.Map(storeToPatch, store);
        await _repository.SaveAsync();
    }

    public async Task UpdateStoreAsync(Guid storeId, StoreUpdateDto storeUpdateDto, bool trackChanges)
    {
        var store = await GetStoreAndCheckIfItExists(storeId, trackChanges);

        _mapper.Map(storeUpdateDto, store);
        await _repository.SaveAsync();
    }
}