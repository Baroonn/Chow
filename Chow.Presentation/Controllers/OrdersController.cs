using Chow.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chow.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiversion}/buyers/{buyerId}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public OrdersController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersForBuyer(Guid buyerId, [FromQuery] OrderParameters orderParameters)
        {
            var pagedResult = await _service.OrderService.GetAllOrdersForBuyerAsync(buyerId, orderParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.buyerOrders);
        }

        [HttpGet("{id:guid}", Name = "GetOrderForBuyer")]
        public async Task<IActionResult> GetOrdersForBuyer(Guid buyerId, Guid id)
        {
            var ordersReadDto = await _service.OrderService.GetOrderForBuyerAsync(buyerId, id, trackChanges: false);
            return Ok(ordersReadDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOrderForBuyer(Guid buyerId, [FromBody] OrderCreateDto orderCreateDto)
        {

            var orderReadDto = await _service.OrderService.CreateOrderForBuyerAsync(buyerId, orderCreateDto, trackChanges: false);
            return CreatedAtRoute("GetOrderForBuyer", new { buyerId, id = orderReadDto.id }, orderReadDto);
        }
    }
}
