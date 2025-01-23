using Microsoft.AspNetCore.Mvc;
using api_painel_producao.Services;
using Microsoft.AspNetCore.Authorization;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;
using api_painel_producao.DTOs;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase {

        private readonly IOrderService _service;

        public OrderController (IOrderService service) {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> CreateOrder ([FromBody] CreateOrderViewModel order) {

            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            ServiceResponse<int> response = await _service.CreateOrderAsync(token, order);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return CreatedAtAction(nameof(GetOrderById), new { id = response.Data }, new { id = response.Data, message = response.Message });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetOrderById ([FromRoute] int id) {
            ServiceResponse<OrderDTO> response = await _service.GetOrderByIdAsync(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
    }
}
