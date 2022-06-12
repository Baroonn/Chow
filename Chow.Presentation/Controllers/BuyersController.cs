using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Chow.Presentation.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Chow.Presentation.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/{v:apiversion}/buyers")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public BuyersController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}", Name = "BuyerById")]
        public async Task<IActionResult> GetBuyer(Guid id)
        {
            
            var buyer = await _service.BuyerService.GetBuyerAsync(id, trackChanges: false);
            return Ok(buyer);
        }

        [HttpGet]
        public async Task<IActionResult> GetBuyers([FromQuery] BuyerParameters buyerParameters)
        {
            var result = await _service.BuyerService.GetAllBuyersAsync(buyerParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
            return Ok(result.buyers);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateBuyer([FromBody] BuyerCreateDto buyerCreateDto)
        {
            var buyerReadDto = await _service.BuyerService.CreateBuyerAsync(buyerCreateDto);
            return CreatedAtRoute("BuyerById", new { id = buyerReadDto.Id }, buyerReadDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBuyer(Guid id)
        {
            await _service.BuyerService.DeleteBuyerAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateBuyer(Guid id, [FromBody] BuyerUpdateDto buyerUpdateDto)
        {
            await _service.BuyerService.UpdateBuyerAsync(id, buyerUpdateDto, trackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdateBuyer(Guid id, [FromBody] JsonPatchDocument<BuyerUpdateDto> patchDoc)
        {
            if (patchDoc is null)
            {
                return BadRequest("patchDoc object sent from client is null.");
            }

            var result = await _service.BuyerService.GetBuyerForPatchAsync(id, trackChanges: true);
            patchDoc.ApplyTo(result.buyerToPatch, ModelState);

            TryValidateModel(result.buyerToPatch);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _service.BuyerService.SaveChangesForPatchAsync(result.buyerToPatch, result.buyer);
            return NoContent();

        }

        
    }
}
