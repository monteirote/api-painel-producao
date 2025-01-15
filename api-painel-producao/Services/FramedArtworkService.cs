using api_painel_producao.Repositories;
using api_painel_producao.Utils;
using api_painel_producao.ViewModels;
using api_painel_producao.DTOs;
using api_painel_producao.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api_painel_producao.Services {

    public interface IFramedArtworkService {
        Task<ServiceResponse<int>> CreateFramedArtwork (CreateFramedArtworkViewModel framedArtwork);
        Task<ServiceResponse<FramedArtworkDTO>> GetArtworkById (int id);
        Task<ServiceResponse<int>> DeleteArtworkById (int id);
    }


    public class FramedArtworkService : IFramedArtworkService {

        private readonly IFramedArtworkRepository _repository;
        private readonly IMaterialRepository _materialRepository;

        public FramedArtworkService (IFramedArtworkRepository repository, IMaterialRepository materialRepository) {
            _repository = repository;
            _materialRepository = materialRepository;
        }

        public async Task<ServiceResponse<int>> CreateFramedArtwork (CreateFramedArtworkViewModel framedArtwork) {
            try {

                if (!await ValidateMaterial(MaterialType.Glass, framedArtwork.GlassId))
                    return ServiceResponse<int>.Fail("This glass does not exist.");

                if (!await ValidateMaterial(MaterialType.Paper, framedArtwork.PaperId))
                    return ServiceResponse<int>.Fail("This paper does not exist.");

                if (!await ValidateMaterial(MaterialType.Background, framedArtwork.BackgroundId))
                    return ServiceResponse<int>.Fail("This background does not exist.");

                if (!await ValidateMaterial(MaterialType.Frame, framedArtwork.FrameId))
                    return ServiceResponse<int>.Fail("This frame does not exist.");

                var dto = FramedArtworkDTO.Create(framedArtwork);

                await _repository.CreateFramedArtwork(dto);

                return ServiceResponse<int>.Ok(dto.Id);

            } catch (Exception e) {
                return ServiceResponse<int>.Fail("Action failed: internal error.");
            }
        }

        public async Task<ServiceResponse<FramedArtworkDTO>> GetArtworkById (int id) {
            try {

                var artworkFound = await _repository.GetFramedArtorkById(id);

                if (artworkFound is null)
                    return ServiceResponse<FramedArtworkDTO>.Fail("Action failed: this artwork does not exist");

                return ServiceResponse<FramedArtworkDTO>.Ok(artworkFound);

            } catch (Exception e) {
                return ServiceResponse<FramedArtworkDTO>.Fail("Action failed: internal error.");
            }
        }

        public async Task<ServiceResponse<int>> DeleteArtworkById (int id) {
            try {

                var artworkFound = await _repository.GetFramedArtorkById(id);

                if (artworkFound is null)
                    return ServiceResponse<int>.Fail("Action failed: this artwork does not exist");

                await _repository.DeleteFramedArtwork(artworkFound.Id);

                return ServiceResponse<int>.Ok();

            } catch (Exception e) {
                return ServiceResponse<int>.Fail("Action failed: internal error.");
            }
        }


        private async Task<bool> ValidateMaterial (MaterialType type, int? id) {

            if (id == null) 
                return true;

            return await _materialRepository.CheckMaterialExistence(type, id.Value);
        }
    }
}
