using Microsoft.AspNetCore.Mvc;
using api_painel_producao.Services;
using Microsoft.AspNetCore.Authorization;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;

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
        }
    }
}
