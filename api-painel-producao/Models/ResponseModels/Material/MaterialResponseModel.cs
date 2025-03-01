using api_painel_producao.DTOs;

namespace api_painel_producao.Models.ResponseModels.Material {
    public class MaterialResponseModel {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public static MaterialResponseModel Create (MaterialDTO materialData) { 
            if (materialData is null) return null;

            return new MaterialResponseModel { 
                Id = materialData.Id,
                Name = materialData.Name,
            };
        }
    }
}
