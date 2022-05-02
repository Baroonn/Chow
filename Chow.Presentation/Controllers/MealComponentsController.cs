using System.Text.Json;
using Chow.Presentation.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Chow.Presentation.Controllers;

[ApiVersion("1.0")]
[Route("api/{v:apiversion}/stores/{storeId}/components")]
[ApiController]
public class MealComponentsController : ControllerBase
{
    private readonly IServiceManager _service;

    public MealComponentsController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    [HttpHead]
    public async Task<IActionResult> GetMealComponentsForStore(Guid storeId, [FromQuery] MealComponentParameters mealComponentParameters)
    {
        var pagedResult = await _service.MealComponentService.GetMealComponentsAsync(storeId, mealComponentParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.paginationMetaData));
        return Ok(pagedResult.mealComponentReadDtos);
    }

    [HttpGet("{id:guid}", Name = "GetMealComponentForStore")]
    public async Task<IActionResult> GetMealComponentForStore(Guid storeId, Guid id)
    {
        var mealComponent = await _service.MealComponentService.GetMealComponentAsync(storeId, id, trackChanges: false);
        return Ok(mealComponent);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateMealComponentForStore(Guid storeId, [FromBody] MealComponentCreateDto mealComponentCreateDto)
    {
        var mealComponentReadDto = await _service.MealComponentService.CreateMealComponentForStoreAsync(storeId, mealComponentCreateDto, trackChanges: false);
        return CreatedAtRoute("GetMealComponentForStore", new { storeId, id = mealComponentReadDto.Id }, mealComponentReadDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMealComponentForStore(Guid storeId, Guid id)
    {
        await _service.MealComponentService.DeleteMealComponentForStoreAsync(storeId, id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateMealComponentForStore(Guid storeId, Guid id, [FromBody] MealComponentUpdateDto mealComponentUpdateDto)
    {
        await _service.MealComponentService.UpdateMealComponentForStoreAsync(storeId, id, mealComponentUpdateDto, trackStoreChanges: false, trackMealComponentChanges: true);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PartiallyUpdateMealComponentForStore(Guid storeId, Guid id, [FromBody] JsonPatchDocument<MealComponentUpdateDto> patchDoc)
    {
        if(patchDoc is null)
        {
            return BadRequest("patchDoc object sent from client is null");
        }

        var result = await _service.MealComponentService.GetMealComponentForPatchAsync(storeId, id, trackStoreChanges: false, trackMealComponentChanges: true);
        patchDoc.ApplyTo(result.mealComponentToPatch, ModelState);
        TryValidateModel(result.mealComponentToPatch);
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        await _service.MealComponentService.SaveChangesForPatchAsync(result.mealComponentToPatch, result.mealComponent);
        return NoContent();
    }
}