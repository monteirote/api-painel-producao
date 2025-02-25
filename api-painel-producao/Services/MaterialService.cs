using api_painel_producao.DTOs;
using api_painel_producao.Repositories;
using api_painel_producao.Utils;
using api_painel_producao.Models.Enums;
using api_painel_producao.Models.RequestModels.Material;

namespace api_painel_producao.Services {

    public interface IMaterialService {
        Task<ServiceResponse<int>> CreateMaterialAsync (MaterialDataRequestModel material);
        Task<ServiceResponse<MaterialDTO>> GetMaterialByIdAsync (int id);
        Task<ServiceResponse<List<MaterialDTO>>> GetMaterialsByType (string type);
        Task<ServiceResponse<int>> DeleteMaterialById (int id);
    }

    public class MaterialService : IMaterialService {

        private readonly IMaterialRepository _repository;

        public MaterialService (IMaterialRepository repository) {
            _repository = repository;
        }

        public async Task<ServiceResponse<int>> CreateMaterialAsync (MaterialDataRequestModel material) {
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

        public async Task<ServiceResponse<List<MaterialDTO>>> GetMaterialsByType (string type) { 
            try {

                bool typeIsValid = Enum.TryParse(type, out MaterialType typeEnum);

                if (!typeIsValid)
                    return ServiceResponse<List<MaterialDTO>>.Fail("Action failed: this type does not exist.");

                var results = await _repository.GetMaterialsByType(typeEnum);

                return ServiceResponse<List<MaterialDTO>>.Ok(results);

            } catch (Exception e) {
                return ServiceResponse<List<MaterialDTO>>.Fail("Action failed: internal error");
            }
        }

        public async Task<ServiceResponse<int>> DeleteMaterialById (int id) { 
            try {

                var materialFound = await _repository.GetMaterialById(id);

                if (materialFound is null)
                    return ServiceResponse<int>.Fail("Action failed: this material does not exist");

                await _repository.DeleteMaterial(id);

                return ServiceResponse<int>.Ok();

            } catch (Exception e) {
                return ServiceResponse<int>.Fail("Action failed: internal error");
            }
        }
    }
}
