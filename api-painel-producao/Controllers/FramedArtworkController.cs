using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api_painel_producao.Services;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;
using api_painel_producao.DTOs;
using api_painel_producao.Models.RequestModels.FramedArtwork;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class FramedArtworkController : ControllerBase {

        private readonly IFramedArtworkService _service;

        public FramedArtworkController (IFramedArtworkService service) {
            _service = service;
        }

        [HttpPost]
        [Authorize (Roles = "Admin, Vendedor")]
        public async Task<IActionResult> CreateFramedArtwork ([FromBody] FramedArtworkDataRequestModel framedArtwork) {

            ServiceResponse<int> response = await _service.CreateFramedArtwork(framedArtwork);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetArtworkById), new { id = response.Data }, new { id = response.Data, message = response.Message });
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> GetArtworkById ([FromRoute] int id) { 

            ServiceResponse<FramedArtworkDTO> response = await _service.GetArtworkById(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> DeleteArtworkById ([FromRoute] int id) { 

            ServiceResponse<int> response = await _service.DeleteArtworkById(id);

            if (!response.Success)
                return NotFound(response);

            return NoContent();
        }
    }
}
