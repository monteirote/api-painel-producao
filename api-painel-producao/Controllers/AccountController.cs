using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_painel_producao.Services;
using api_painel_producao.Utils;

using api_painel_producao.Models.RequestModels.Login;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase {



        private readonly IAccountService _service;

        public AccountController (IAccountService service) {
            _service = service;
        }

       // Endpoints

       [HttpPost("signup")]
        public async Task<IActionResult> SignUp ([FromBody] SignupRequestModel userData) {

            ServiceResponse<int> response = await _service.CreateUserAsync(userData);

            if (!response.Success)
                return BadRequest(new { response.Success, response.Message });

            return Ok(new { response.Success, response.Message });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] LoginRequestModel userData) {

            ServiceResponse<string> response = await _service.LoginAsync(userData);

            if (!response.Success)
                return Unauthorized(response);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(12)
            };


            Response.Cookies.Append("jwt", response.Data, cookieOptions);
            response.Data = "";
            return Ok(response);
        }


        [HttpPut ("{id}/deactivate")]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<IActionResult> DeactivateAccount ([FromRoute] int id) { 

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            ServiceResponse<string> response = await _service.DeactivateUserAsync(token, id);

            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }


        [HttpPut ("{id}/activate")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> ActivateAccount ([FromRoute] int id) {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            ServiceResponse<string> response = await _service.ActivateUserAsync(token, id);

            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }


        [HttpPut ("{id}/change-password")]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<IActionResult> ChangePassword ([FromRoute] int id, [FromBody] ChangePasswordRequestModel userData) {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            ServiceResponse<string> response = await _service.ChangePasswordAsync(token, id, userData.NewPassword, userData.OldPassword);

            if (response.PermissionDenied)
                return Forbid(response.Message);

            if (!response.Success)
                return BadRequest(response.Message);

            return NoContent();
        }


        [HttpGet("pending-approval")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UsersWaitingActivation () {
            var users = await _service.RetrieveUsersPendingApproval();

            return Ok(users);
        }
    }
}
