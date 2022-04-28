using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class BuyerService : IBuyerService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public BuyerService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
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


    public async Task<BuyerReadDto> CreateBuyerAsync(BuyerCreateDto buyerCreateDto)
    {
        var buyer = _mapper.Map<Buyer>(buyerCreateDto);
        _repository.Buyer.CreateBuyer(buyer);
        await _repository.SaveAsync();
        var buyerReadDto = _mapper.Map<BuyerReadDto>(buyer);
        return buyerReadDto;
    }

    public async Task DeleteBuyerAsync(Guid buyerId, bool trackChanges)
    {
        var buyer = await GetBuyerAndCheckIfItExists(buyerId, trackChanges);
        _repository.Buyer.DeleteBuyer(buyer);
        await _repository.SaveAsync();
    }
    
    public async Task<(IEnumerable<BuyerReadDto> buyers, PaginationMetaData metaData)> GetAllBuyersAsync(BuyerParameters buyerParameters, bool trackChanges)
    {
        var buyers = await _repository.Buyer.GetAllBuyersAsync(buyerParameters, trackChanges);
        var buyersReadDto = _mapper.Map<IEnumerable<BuyerReadDto>>(buyers);
        return (buyers: buyersReadDto, metaData: buyers.PaginationMetaData);  
    }

    public async Task<BuyerReadDto> GetBuyerAsync(Guid buyerId, bool trackChanges)
    {
        var buyer = await GetBuyerAndCheckIfItExists(buyerId, trackChanges);
        var buyerReadDto = _mapper.Map<BuyerReadDto>(buyer);
        return buyerReadDto;
    }

    public async Task<(BuyerUpdateDto buyerToPatch, Buyer buyer)> GetBuyerForPatchAsync(Guid buyerId, bool trackChanges)
    {
        var buyer = await GetBuyerAndCheckIfItExists(buyerId, trackChanges);
        var buyerToPatch = _mapper.Map<BuyerUpdateDto>(buyer);
        return (buyerToPatch, buyer);
    }

    public async Task SaveChangesForPatchAsync(BuyerUpdateDto buyerToPatch, Buyer buyer)
    {
        _mapper.Map(buyerToPatch, buyer);
        await _repository.SaveAsync();
    }

    public async Task UpdateBuyerAsync(Guid buyerId, BuyerUpdateDto buyerUpdateDto, bool trackChanges)
    {
        var buyer = await GetBuyerAndCheckIfItExists(buyerId, trackChanges);
        _mapper.Map(buyerUpdateDto, buyer);
        await _repository.SaveAsync();
    }
}