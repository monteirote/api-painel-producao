using api_painel_producao.DTOs;
using api_painel_producao.Models.RequestModels.Material;
using api_painel_producao.Services;
using api_painel_producao.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase {

        private readonly IMaterialService _service;
            
        public MaterialController (IMaterialService service) {
            _service = service;
        }


        [HttpPost]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<ActionResult> CreateMaterial ([FromBody] MaterialDataRequestModel material) {

            ServiceResponse<int> response = await _service.CreateMaterialAsync(material);

            if (!response.Success)
                return BadRequest(new { message = response.Message });

            return CreatedAtAction(nameof(GetMaterialById), new { id = response.Data }, new { id = response.Data, message = response.Message });
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetMaterialById ([FromRoute] int id) {

            ServiceResponse<MaterialDTO> response = await _service.GetMaterialByIdAsync(id);

            if (!response.Success)
                NotFound();
            
            return Ok(response);
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetMaterialsByType ([FromQuery] string type) {

            ServiceResponse<List<MaterialDTO>> response = await _service.GetMaterialsByType(type);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpDelete]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<IActionResult> DeleteMaterialById ([FromRoute] int id) {

            ServiceResponse<int> response = await _service.DeleteMaterialById(id);

            if (!response.Success)
                return NotFound();

            return NoContent();
        }
    }
}

