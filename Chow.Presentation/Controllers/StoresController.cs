using Chow.Presentation.ModelBinders;
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
    public IActionResult GetStores()
    {
        var stores = _service.StoreService.GetAllStores(trackChanges: false);
        return Ok(stores);
    }

    [HttpGet("{id:guid}", Name="StoreById")]
    public IActionResult GetStore(Guid id)
    {
        var store = _service.StoreService.GetStore(id, trackChanges: false);
        return Ok(store);
    }

    [HttpPost]
    public IActionResult CreateStore([FromBody] StoreCreateDto storeCreateDto)
    {
        if(storeCreateDto is null)
        {
            return BadRequest("Store parameters are not complete");
        }

        if(!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var createdStore = _service.StoreService.CreateStore(storeCreateDto);
        return CreatedAtRoute("StoreById", new { id = createdStore.Id }, createdStore);
    }

    [HttpGet("collection/({ids})", Name = "StoreCollection")]
    public IActionResult GetStoreCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var storesReadDto = _service.StoreService.GetStoresByIds(ids, trackChanges: false);
        return Ok(storesReadDto);
    }

    [HttpPost("collection")]
    public IActionResult CreateStoreCollection([FromBody] IEnumerable<StoreCreateDto> storeCreateDtoCollection)
    {
        var result = _service.StoreService.CreateStoreCollection(storeCreateDtoCollection);
        return CreatedAtRoute("StoreCollection", new { result.ids }, result.storeReadDtoCollection);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteStore(Guid id)
    {
        _service.StoreService.DeleteStore(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateStore(Guid id, [FromBody] StoreUpdateDto storeUpdateDto)
    {
        if (storeUpdateDto is null)
        {
            return BadRequest("StoreUpdateDto is null");
        }

        _service.StoreService.UpdateStore(id, storeUpdateDto, trackChanges: true);
        return NoContent();
    }

}
