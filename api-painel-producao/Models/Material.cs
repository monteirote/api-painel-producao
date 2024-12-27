using api_painel_producao.Models.Enums;

namespace api_painel_producao.Models {

    public class Material {

        public int Id { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }


        public MaterialType Type { get; set; }

        public MeasurementUnit MeasurementUnit { get; set; }

        public decimal ValueByUnit { get; set; }

    }
}
