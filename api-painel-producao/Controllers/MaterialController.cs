using api_painel_producao.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController (IMaterialService service) : ControllerBase {

        private readonly IMaterialService _service = service;

        [HttpPost]
        [Authorize (Roles = "Admin, Vendedor")]
        public ActionResult CreateMaterial () {
            return null;
        }
    }
}
