using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Chow.Presentation.Controllers;

[Route("api/stores/{storeId}/components")]
[ApiController]
public class MealComponentsController : ControllerBase
{
    private readonly IServiceManager _service;

    public MealComponentsController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetMealComponentsForStore(Guid storeId)
    {
        var mealComponents = await _service.MealComponentService.GetMealComponentsAsync(storeId, trackChanges: false);
        return Ok(mealComponents);
    }

    [HttpGet("{id:guid}", Name = "GetMealComponentForStore")]
    public async Task<IActionResult> GetMealComponentForStore(Guid storeId, Guid id)
    {
        var mealComponent = await _service.MealComponentService.GetMealComponentAsync(storeId, id, trackChanges: false);
        return Ok(mealComponent);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMealComponentForStore(Guid storeId, [FromBody] MealComponentCreateDto mealComponentCreateDto)
    {
        if (mealComponentCreateDto is null)
        {
            return BadRequest("MealComponentCreateDto is null");
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

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
    public async Task<IActionResult> UpdateMealComponentForStore(Guid storeId, Guid id, [FromBody] MealComponentUpdateDto mealComponentUpdateDto)
    {
        if (mealComponentUpdateDto is null)
        {
            return BadRequest("MealComponentUpdateDto object is null");
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

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