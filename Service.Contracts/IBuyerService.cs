using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IBuyerService
{
    Task<(IEnumerable<BuyerReadDto> buyers, PaginationMetaData metaData) > GetAllBuyersAsync(BuyerParameters buyerParameters, bool trackChanges);
    Task<BuyerReadDto> GetBuyerAsync(Guid buyerId, bool trackChanges);
    Task<BuyerReadDto> CreateBuyerAsync(BuyerCreateDto buyerCreateDto);
    Task DeleteBuyerAsync(Guid buyerId, bool trackChanges);
    Task UpdateBuyerAsync(Guid buyerId, BuyerUpdateDto buyerUpdateDto, bool trackChanges);
    Task<(BuyerUpdateDto buyerToPatch, Buyer buyer)> GetBuyerForPatchAsync(Guid buyerId, bool trackChanges);
    Task SaveChangesForPatchAsync(BuyerUpdateDto buyerToPatch, Buyer buyer);
}