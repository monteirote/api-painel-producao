using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_painel_producao.ViewModels;
using api_painel_producao.Services;
using api_painel_producao.Utils;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase {

        private readonly IAccountService _service;

        public AccountController (IAccountService service) {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp ([FromBody] UserSignupViewModel userData) {

            ServiceResponse<int> response = await _service.CreateUserAsync(userData);

            if (!response.Success)
                return BadRequest(new { response.Success, response.Message });

            return Ok(new { response.Success, response.Message });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] UserLoginViewModel userData) { 
            ServiceResponse<string> response = await _service.LoginAsync(userData);
            
            if (!response.Success)
                return Unauthorized(response);
            
            return Ok(response);
        }


        [HttpPost ("deactivate")]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<IActionResult> DeactivateAccount ([FromQuery] int id) { 

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { Success = false, Message = "Token is missing or invalid." });

            ServiceResponse<string> response = await _service.DeactivateUserAsync(token, id);

            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }


    }
}
