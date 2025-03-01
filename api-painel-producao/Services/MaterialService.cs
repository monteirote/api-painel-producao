using api_painel_producao.DTOs;
using api_painel_producao.Repositories;
using api_painel_producao.Utils;
using api_painel_producao.Models.Enums;
using api_painel_producao.Models.RequestModels.Material;
using api_painel_producao.Models.ResponseModels.Material;

namespace api_painel_producao.Services {

    public interface IMaterialService {
        Task<ServiceResponse<int>> CreateMaterialAsync (MaterialDataRequestModel material);
        Task<ServiceResponse<DetailedMaterialResponseModel>> GetMaterialByIdAsync (int id);
        Task<ServiceResponse<List<DetailedMaterialResponseModel>>> GetMaterialsByType (string type);
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

        public async Task<ServiceResponse<DetailedMaterialResponseModel>> GetMaterialByIdAsync (int id) {
            try {

                MaterialDTO materialFound = await _repository.GetMaterialById(id);

                if (materialFound is null)
                    return ServiceResponse<DetailedMaterialResponseModel>.Fail("Action failed: This material does not exist.");

                return ServiceResponse<DetailedMaterialResponseModel>.Ok(DetailedMaterialResponseModel.Create(materialFound));

            } catch (Exception e) {
                return ServiceResponse<DetailedMaterialResponseModel>.Fail("Action failed: internal error");
            }
        }

        public async Task<ServiceResponse<List<DetailedMaterialResponseModel>>> GetMaterialsByType (string type) { 
            try {

                bool typeIsValid = Enum.TryParse(type, out MaterialType typeEnum);

                if (!typeIsValid)
                    return ServiceResponse<List<DetailedMaterialResponseModel>>.Fail("Action failed: this type does not exist.");

                var materials = await _repository.GetMaterialsByType(typeEnum);

                var results = materials.Select(x => DetailedMaterialResponseModel.Create(x)).ToList();

                return ServiceResponse<List<DetailedMaterialResponseModel>>.Ok(results);

            } catch (Exception e) {
                return ServiceResponse<List<DetailedMaterialResponseModel>>.Fail("Action failed: internal error");
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
