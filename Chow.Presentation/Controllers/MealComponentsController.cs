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
    public IActionResult GetMealComponentsForStore(Guid storeId)
    {
        var mealComponents = _service.MealComponentService.GetMealComponents(storeId, trackChanges: false);
        return Ok(mealComponents);
    }

    [HttpGet("{id:guid}", Name = "GetMealComponentForStore")]
    public IActionResult GetMealComponentForStore(Guid storeId, Guid id)
    {
        var mealComponent = _service.MealComponentService.GetMealComponent(storeId, id, trackChanges: false);
        return Ok(mealComponent);
    }

    [HttpPost]
    public IActionResult CreateMealComponentForStore(Guid storeId, [FromBody] MealComponentCreateDto mealComponentCreateDto)
    {
        if (mealComponentCreateDto is null)
        {
            return BadRequest("MealComponentCreateDto is null");
        }

        var mealComponentReadDto = _service.MealComponentService.CreateMealComponentForStore(storeId, mealComponentCreateDto, trackChanges: false);
        return CreatedAtRoute("GetMealComponentForStore", new { storeId, id = mealComponentReadDto.Id }, mealComponentReadDto);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteMealComponentForStore(Guid storeId, Guid id)
    {
        _service.MealComponentService.DeleteMealComponentForStore(storeId, id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateMealComponentForStore(Guid storeId, Guid id, [FromBody] MealComponentUpdateDto mealComponentUpdateDto)
    {
        if (mealComponentUpdateDto is null)
        {
            return BadRequest("MealComponentUpdateDto object is null");
        }

        _service.MealComponentService.UpdateMealComponentForStore(storeId, id, mealComponentUpdateDto, trackStoreChanges: false, trackMealComponentChanges: true);
        return NoContent();
    }
}