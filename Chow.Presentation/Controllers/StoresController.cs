using System.Text.Json;
using Chow.Presentation.ActionFilters;
using Chow.Presentation.ModelBinders;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Chow.Presentation.Controllers;

[ApiVersion("1.0")]
[Route("api/{v:apiversion}/stores")]
[ApiController]
//[ResponseCache(CacheProfileName = "120SecondsDuration")]
public class StoresController : ControllerBase
{
    private readonly IServiceManager _service;

    public StoresController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    [HttpHead]
    public async Task<IActionResult> GetStores([FromQuery] StoreParameters storeParameters)
    {
        var result = await _service.StoreService.GetAllStoresAsync(storeParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
        return Ok(result.storeReadDtos);
    }

    [HttpGet("{id:guid}", Name="StoreById")]
    //[ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetStore(Guid id)
    {
        var store = await _service.StoreService.GetStoreAsync(id, trackChanges: false);
        return Ok(store);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateStore([FromBody] StoreCreateDto storeCreateDto)
    {
        var createdStore = await _service.StoreService.CreateStoreAsync(storeCreateDto);
        return CreatedAtRoute("StoreById", new { id = createdStore.Id }, createdStore);
    }

    [HttpGet("collection/({ids})", Name = "StoreCollection")]
    public async Task<IActionResult> GetStoreCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var storesReadDto = await _service.StoreService.GetStoresByIdsAsync(ids, trackChanges: false);
        return Ok(storesReadDto);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateStoreCollection([FromBody] IEnumerable<StoreCreateDto> storeCreateDtoCollection)
    {
        var result = await _service.StoreService.CreateStoreCollectionAsync(storeCreateDtoCollection);
        return CreatedAtRoute("StoreCollection", new { result.ids }, result.storeReadDtoCollection);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStore(Guid id)
    {
        await _service.StoreService.DeleteStoreAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateStore(Guid id, [FromBody] StoreUpdateDto storeUpdateDto)
    {
        await _service.StoreService.UpdateStoreAsync(id, storeUpdateDto, trackChanges: true);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PartiallyUpdateStore(Guid id, [FromBody] JsonPatchDocument<StoreUpdateDto> patchDoc)
    {
        if(patchDoc is null)
        {
            return BadRequest("patchDoc object sent from client is null");
        }

        var result = await _service.StoreService.GetStoreForPatchAsync(id, trackChanges: true);
        patchDoc.ApplyTo(result.storeToPatch, ModelState);
        TryValidateModel(result.storeToPatch);
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        await _service.StoreService.SaveChangesForPatchAsync(result.storeToPatch, result.store);
        return NoContent();

    }

    [HttpGet("{id:guid}/orders")]
    [HttpHead]
    public async Task<IActionResult> GetAllOrdersForStore(Guid id, [FromQuery] OrderParameters orderParameters)
    {
        var pagedResult = await _service.OrderService.GetAllOrdersForStoreAsync(id, orderParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.storeOrders);
    }

    [HttpGet("{id:guid}/orders/{orderId:guid}")]
    public async Task<IActionResult> GetAllOrdersForStore(Guid id, Guid orderId)
    {
        var order = await _service.OrderService.GetOrderForStoreAsync(id, orderId, trackChanges: false);
        return Ok(order);
    }

    [HttpOptions]
    public IActionResult GetStoresOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");
        return Ok();
    }

}
