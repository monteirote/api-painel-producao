using api_painel_producao.Repositories;
using api_painel_producao.Utils;
using api_painel_producao.ViewModels;
using api_painel_producao.DTOs;

namespace api_painel_producao.Services {

    public interface IFramedArtworkService {
        ServiceResponse<int> CreateFramedArtwork (CreateFramedArtworkViewModel framedArtwork);
    }


    public class FramedArtworkService : IFramedArtworkService {

        private readonly IFramedArtworkRepository _repository;
        private readonly IMaterialRepository _materialRepository;

        public FramedArtworkService (IFramedArtworkRepository repository, IMaterialRepository materialRepository) {
            _repository = repository;
            _materialRepository = materialRepository;
        }

        public async ServiceResponse<int> CreateFramedArtwork (CreateFramedArtworkViewModel framedArtwork) {
            try {

                var glassExists = _materialRepository.CheckMaterialExistence()
                


            } catch (Exception e) {
                return ServiceResponse<int>.Fail("Failed to create customer.");
            }
        }
    }
}
