using api_painel_producao.Models;

namespace api_painel_producao.DTOs {
    public class FramedArtworkDTO {
        public int Id { get; set; }

        public float Height { get; set; }
        public float Width { get; set; }

        public decimal TotalPrice { get; set; }

        public string ImageDescription { get; set; }

        public MaterialDTO Glass { get; set; }
        public MaterialDTO Frame { get; set; }
        public MaterialDTO Background { get; set; }
        public MaterialDTO Paper { get; set; }

        public FramedArtworkDTO (FramedArtwork framedArtworkData) {
            this.Id = framedArtworkData.Id;
            this.Height = framedArtworkData.Height;
            this.Width = framedArtworkData.Width;
            this.TotalPrice = framedArtworkData.TotalPrice;
            this.ImageDescription = framedArtworkData.ImageDescription;
            this.Glass = new MaterialDTO (framedArtworkData.Glass);
            this.Frame = new MaterialDTO (framedArtworkData.Frame);
            this.Background = new MaterialDTO (framedArtworkData.Background);
            this.Paper = new MaterialDTO (framedArtworkData.Paper);
        }
    }
}
