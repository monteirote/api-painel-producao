using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api_painel_producao.Services;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;

namespace api_painel_producao.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class FramedArtworkController : ControllerBase {

        private readonly IFramedArtworkService _service;

        public FramedArtworkController (IFramedArtworkService service) {
            _service = service;
        }

        public async Task<IActionResult> CreateFramedArtwork (CreateFramedArtworkViewModel framedArtwork) {

            ServiceResponse<int> response = await _service.

        }
    }
}
