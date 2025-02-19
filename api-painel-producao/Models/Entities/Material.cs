using api_painel_producao.Models.Enums;

namespace api_painel_producao.Models {

    public class Material {

        public int Id { get; set; }


        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;


        public MaterialType Type { get; set; }

        public MeasurementUnit MeasurementUnit { get; set; }

        public decimal ValueByUnit { get; set; }

    }
}
