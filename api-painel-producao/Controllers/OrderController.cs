using Microsoft.AspNetCore.Mvc;
using api_painel_producao.Services;
using Microsoft.AspNetCore.Authorization;
using api_painel_producao.Utils;
using api_painel_producao.DTOs;
using api_painel_producao.Models.RequestModels.Order;
using api_painel_producao.Models.ResponseModels.Order;

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
        public async Task<IActionResult> CreateOrder ([FromBody] OrderDataRequestModel order) {

            var token = Request.Cookies["jwt"];

            ServiceResponse<int> response = await _service.CreateOrderAsync(token, order);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return CreatedAtAction(nameof(GetOrderById), new { id = response.Data }, new { id = response.Data, message = response.Message });
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetOrderById ([FromRoute] int id) {
            ServiceResponse<DetailedOrderResponseModel> response = await _service.GetOrderByIdAsync(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetAllOrders() {
            var orders = await _service.GetAllOrders();

            return Ok(orders);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> CancelOrderById([FromRoute] int id) {
            var token = Request.Cookies["jwt"];

            ServiceResponse<int> response = await _service.CancelOrderById(id, token);

            if (!response.Success)
                return BadRequest(response.Message);

            return NoContent();
        }
    }
}
