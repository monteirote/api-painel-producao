using api_painel_producao.Services;
using api_painel_producao.ViewModels;
using api_painel_producao.DTOs;
using api_painel_producao.Models;
using api_painel_producao.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_painel_producao.Models.RequestModels.Customer;


namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase {

        private readonly ICustomerService _service;

        public CustomerController (ICustomerService service) {
            _service = service;
        }


        // Endpoints

        [HttpGet]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetAllCustomersAsync () {

            ServiceResponse<List<CustomerDTO>> foundCustomers = await _service.FindAllCustomersAsync();

            return Ok(foundCustomers);
        }



        [HttpGet("{id}")]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetCustomerById ([FromRoute] int id) {

            ServiceResponse<CustomerDTO> response = await _service.GetCustomerById(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> CreateCustomer ([FromBody] CustomerDataRequestModel newCustomerData) {

            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            ServiceResponse<int> response = await _service.CreateCustomerAsync(token, newCustomerData);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return CreatedAtAction(nameof(GetCustomerById), new { id = response.Data }, new { id = response.Data, message = response.Message });
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> UpdateCustomer ([FromRoute] int id, [FromBody] CustomerDataRequestModel newCustomerData) {

            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            ServiceResponse<Customer> response = await _service.UpdateCustomerById(id, token, newCustomerData);

            if (response.PermissionDenied)
                return Forbid(response.Message);

            if (!response.Success)
                return BadRequest(response);

            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> DeleteCustomer ([FromRoute] int id) { 
        
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            ServiceResponse<int> response = await _service.DeleteCustomer(id, token);

            if (!response.Success)
                return BadRequest(response.Message);

            return NoContent();
        }
    }
}
