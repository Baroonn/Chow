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

    public StoreReadDto CreateStore(StoreCreateDto storeCreateDto)
    {
        var store = _mapper.Map<Store>(storeCreateDto);
        _repository.Store.CreateStore(store);
        _repository.Save();

        var storeReadDto = _mapper.Map<StoreReadDto>(store);
        return storeReadDto;
    }

    public (IEnumerable<StoreReadDto> storeReadDtoCollection, string ids) CreateStoreCollection(IEnumerable<StoreCreateDto> storeCreateDtoCollection)
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
        _repository.Save();

        var storeReadDtoCollection = _mapper.Map<IEnumerable<StoreReadDto>>(storeCollection);
        var ids = string.Join(',', storeReadDtoCollection.Select(s => s.Id));

        return (storeReadDtoCollection: storeReadDtoCollection, ids: ids);
    }

    public void DeleteStore(Guid storeId, bool trackChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackChanges);
        if (store is null)
        {
            throw new StoreNotFoundException(storeId);
        }

        _repository.Store.DeleteStore(store);
        _repository.Save();
    }

    public IEnumerable<StoreReadDto> GetAllStores(bool trackChanges)
    {
        
        var stores = _repository.Store.GetAllStores(trackChanges);
        var storesReadDto = _mapper.Map<IEnumerable<StoreReadDto>>(stores);
        return storesReadDto;
    }

    public StoreReadDto GetStore(Guid storeId, bool trackChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackChanges);
        if (store is null)
        {
            throw new StoreNotFoundException(storeId);
        }
        var storeReadDto = _mapper.Map<StoreReadDto>(store);
        return storeReadDto;
    }

    public IEnumerable<StoreReadDto> GetStoresByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
        {
            throw new IdParametersBadRequestException();
        }

        var stores = _repository.Store.GetStoresByIds(ids, trackChanges);
        if (ids.Count() != stores.Count())
        {
            throw new CollectionsByIdsBadRequestException();
        }
        var storesReadDto = _mapper.Map<IEnumerable<StoreReadDto>>(stores);
        return storesReadDto;
    }

    public void UpdateStore(Guid storeId, StoreUpdateDto storeUpdateDto, bool trackChanges)
    {
        var store = _repository.Store.GetStore(storeId, trackChanges);
        if(store is null)
        {
            throw new StoreNotFoundException(storeId);
        }

        _mapper.Map(storeUpdateDto, store);
        _repository.Save();
    }
}