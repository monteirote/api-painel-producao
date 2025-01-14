using api_painel_producao.DTOs;
using api_painel_producao.Services;
using api_painel_producao.Utils;
using api_painel_producao.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController (IMaterialService service) : ControllerBase {


        private readonly IMaterialService _service = service;

        [HttpPost]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<ActionResult> CreateMaterial ([FromBody] CreateMaterialViewModel material) {

            ServiceResponse<int> response = await _service.CreateMaterialAsync(material);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return CreatedAtAction(nameof(GetMaterialById), new { id = response.Data }, new { id = response.Data, message = response.Message });
        }


        [HttpGet("/{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetMaterialById ([FromRoute] int id) {

            ServiceResponse<MaterialDTO> response = await _service.GetMaterialByIdAsync(id);

            if (!response.Success)
                NotFound();
            
            return Ok(response);
        }
    }
}
