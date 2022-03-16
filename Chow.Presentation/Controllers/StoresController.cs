using Chow.Presentation.ModelBinders;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Chow.Presentation.Controllers;

[Route("api/stores")]
[ApiController]
public class StoresController : ControllerBase
{
    private readonly IServiceManager _service;

    public StoresController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetStores()
    {
        var stores = await _service.StoreService.GetAllStoresAsync(trackChanges: false);
        return Ok(stores);
    }

    [HttpGet("{id:guid}", Name="StoreById")]
    public async Task<IActionResult> GetStore(Guid id)
    {
        var store = await _service.StoreService.GetStoreAsync(id, trackChanges: false);
        return Ok(store);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStore([FromBody] StoreCreateDto storeCreateDto)
    {
        if(storeCreateDto is null)
        {
            return BadRequest("Store parameters are not complete");
        }

        if(!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

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
    public async Task<IActionResult> UpdateStore(Guid id, [FromBody] StoreUpdateDto storeUpdateDto)
    {
        if (storeUpdateDto is null)
        {
            return BadRequest("StoreUpdateDto is null");
        }

        if(!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

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

}
