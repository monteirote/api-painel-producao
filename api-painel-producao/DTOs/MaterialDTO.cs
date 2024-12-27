using api_painel_producao.Models;

namespace api_painel_producao.DTOs {
    public class MaterialDTO {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string MeasurementUnit { get; set; }
        public decimal ValueByUnit { get; set; }


        public MaterialDTO (Material materialData) {
            this.Id = materialData.Id;
            this.Name = materialData.Name;
            this.Description = materialData.Description;
            this.Type = materialData.Type.ToString();
            this.MeasurementUnit = materialData.MeasurementUnit.ToString();
            this.ValueByUnit = materialData.ValueByUnit;
        }
    }
}
