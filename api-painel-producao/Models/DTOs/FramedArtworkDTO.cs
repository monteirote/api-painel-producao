using api_painel_producao.Models;
using api_painel_producao.Models.RequestModels.FramedArtwork;
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

        public static FramedArtworkDTO Create (FramedArtworkDataRequestModel framedArtwork) {

            if (framedArtwork is null)
                return null;

            return new FramedArtworkDTO {
                Height = framedArtwork.Height,
                Width = framedArtwork.Width,
                GlassId = framedArtwork.GlassId ?? 0,
                FrameId = framedArtwork.FrameId ?? 0,
                BackgroundId = framedArtwork.BackgroundId ?? 0,
                PaperId = framedArtwork.PaperId ?? 0,
            };
        }

        public static FramedArtworkDTO Create(FramedArtwork framedArtwork) {

            if (framedArtwork is null)
                return null;
            
            return new FramedArtworkDTO {
                Id = framedArtwork.Id,
                Height = framedArtwork.Height,
                Width = framedArtwork.Width,
                TotalPrice = framedArtwork.TotalPrice,
                ImageDescription = framedArtwork.ImageDescription,
                Glass = MaterialDTO.Create (framedArtwork.Glass),
                Frame = MaterialDTO.Create (framedArtwork.Frame),
                Background = MaterialDTO.Create (framedArtwork.Background),
                Paper = MaterialDTO.Create (framedArtwork.Paper)
            };
        }
    }

}
