using api_painel_producao.DTOs;
using api_painel_producao.Repositories;
using api_painel_producao.Utils;
using api_painel_producao.ViewModels;

namespace api_painel_producao.Services {

    public interface IMaterialService {
        Task<ServiceResponse<int>> CreateMaterialAsync (CreateMaterialViewModel material);
        Task<ServiceResponse<MaterialDTO>> GetMaterialByIdAsync (int id);
    }

    public class MaterialService : IMaterialService {

        private readonly IMaterialRepository _repository;

        public MaterialService (IMaterialRepository repository) {
            _repository = repository;
        }

        public async Task<ServiceResponse<int>> CreateMaterialAsync (CreateMaterialViewModel material) {
            try {
                var materialToAdd = MaterialDTO.Create(material);

                await _repository.CreateMaterial(materialToAdd);

                return ServiceResponse<int>.Ok(materialToAdd.Id, "Material created successfully.");

            } catch (Exception ex) {
                return ServiceResponse<int>.Fail("Failed to create material.");
            }
        }

        public async Task<ServiceResponse<MaterialDTO>> GetMaterialByIdAsync (int id) {
            try {

                MaterialDTO materialFound = await _repository.GetMaterialById(id);

                if (materialFound is null)
                    return ServiceResponse<MaterialDTO>.Fail("Action failed: This material does not exist.");

                return ServiceResponse<MaterialDTO>.Ok(materialFound);

            } catch (Exception e) {
                return ServiceResponse<MaterialDTO>.Fail("Action failed: internal error");
            }
        }
    }
}
