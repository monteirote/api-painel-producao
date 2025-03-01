using api_painel_producao.DTOs;
using api_painel_producao.Models.ResponseModels.Material;

namespace api_painel_producao.Models.ResponseModels.FramedArtwork {
    public class FramedArtworkResponseModel {

        public int Id { get; set; }

        public float Height { get; set; }
        public float Width { get; set; }
        public decimal TotalPrice { get; set; }

        public int? OrderId { get; set; }

        public MaterialResponseModel? Glass { get; set; }
        public MaterialResponseModel? Frame { get; set; }
        public MaterialResponseModel? Background { get; set; }
        public MaterialResponseModel? Paper { get; set; }

        public static FramedArtworkResponseModel Create(FramedArtworkDTO framedArtworkData) {
            if (framedArtworkData is null) return null;

            return new FramedArtworkResponseModel {
                Id = framedArtworkData.Id,
                Height = framedArtworkData.Height,
                Width = framedArtworkData.Width,
                TotalPrice = framedArtworkData.TotalPrice,
                OrderId = framedArtworkData.OrderId,
                Glass = MaterialResponseModel.Create(framedArtworkData.Glass),
                Frame = MaterialResponseModel.Create(framedArtworkData.Frame),
                Background = MaterialResponseModel.Create(framedArtworkData.Background),
                Paper = MaterialResponseModel.Create(framedArtworkData.Paper),
            };
        }
    }
}
