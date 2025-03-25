using System.ComponentModel.DataAnnotations;

namespace api_painel_producao.Models.RequestModels.FramedArtwork {
   public class FramedArtworkDataRequestModel {

        [Required]
        [Range(0.01, 1000000.00, ErrorMessage = "Action failed: the value of 'Height' is not valid.")]
        public float Height { get; set; }


        [Required]
        [Range(0.01, 1000000.00, ErrorMessage = "Action failed: the value of 'Width' is not valid.")]
        public float Width { get; set; }


        [Range(0.01, 1000000.00, ErrorMessage = "Action failed: the value of 'Price' is not valid.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100)]
        public int? Quantity { get; set; }

        public List<IFormFile> Images { get; set; }

        public int? GlassId { get; set; }

        public int? FrameId { get; set; }

        public int? BackgroundId { get; set; }

        public int? PaperId { get; set; }

   }
}
