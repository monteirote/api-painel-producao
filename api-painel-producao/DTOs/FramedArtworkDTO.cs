using api_painel_producao.Models;
using api_painel_producao.ViewModels;

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

        public int GlassId { get; set; }
        public int FrameId { get; set; }
        public int BackgroundId { get; set; }
        public int PaperId { get; set; }


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

        private FramedArtworkDTO() { }

        public static FramedArtworkDTO Create (CreateFramedArtworkViewModel framedArtwork) {

            return new FramedArtworkDTO {
                Height = framedArtwork.Height,
                Width = framedArtwork.Width,
                GlassId = framedArtwork.GlassId,
                FrameId = framedArtwork.FrameId,
                BackgroundId = framedArtwork.BackgroundId,
                PaperId = framedArtwork.PaperId,
            };
        }

    }

}
