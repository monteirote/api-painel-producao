using api_painel_producao.Services;
using api_painel_producao.ViewModels;
using api_painel_producao.Models;
using api_painel_producao.Utils;
using Microsoft.AspNetCore.Mvc;


namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase {

        private readonly ICustomerService _service;

        public CustomerController (ICustomerService service) {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById ([FromRoute] int id) {

            ServiceResponse<Customer> response = await _service.GetCustomerById(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }



        [HttpPost]
        public async Task<IActionResult> CreateCustomer ([FromBody] CreateCustomerViewModel newCustomer) {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            ServiceResponse<int> response = await _service.CreateCustomerAsync(token, newCustomer);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return CreatedAtAction(nameof(GetCustomerById), new { îd = response.Data }, new { id = response.Data, message = response.Message });
        }
    }
}
