using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_painel_producao.ViewModels;
using api_painel_producao.Services;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase {

        private readonly IAccountService _service;

        public AccountController (IAccountService service) {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp ([FromBody] UserSignupViewModel newUser) {
            try {
                await _service.CreateUserAsync(newUser);

                return Ok(newUser);
            } catch (Exception e) {
                return BadRequest();
            }
        }


    }
}
