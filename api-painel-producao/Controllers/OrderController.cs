using Microsoft.AspNetCore.Mvc;
using api_painel_producao.Services;
using Microsoft.AspNetCore.Authorization;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase {

        private readonly IOrderService _service;

        public OrderController (IOrderService service) {
            _service = service;
        }

        [HttpPost]
        [Authorize (Roles = "Admin, Vendedor")]
        pu
    }
}
