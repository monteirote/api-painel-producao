using api_painel_producao.DTOs;

namespace api_painel_producao.Models.ResponseModels.Material {
    public class DetailedMaterialResponseModel {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string MeasurementUnit { get; set; } = string.Empty;

        public decimal ValueByUnit { get; set; } = 0;

        public static DetailedMaterialResponseModel Create (MaterialDTO materialData) {
            if (materialData == null) return null;

            return new DetailedMaterialResponseModel { 
                Id = materialData.Id,
                Name = materialData.Name,
                Description = materialData.Description,
                Type = materialData.Type,
                MeasurementUnit = materialData.MeasurementUnit,
                ValueByUnit = materialData.ValueByUnit
            };
        }
    }
}
