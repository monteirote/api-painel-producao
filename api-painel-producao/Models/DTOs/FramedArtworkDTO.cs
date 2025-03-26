using api_painel_producao.Models;
using api_painel_producao.Models.RequestModels.FramedArtwork;
using api_painel_producao.ViewModels;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace api_painel_producao.DTOs {
    public class FramedArtworkDTO {
        public int Id { get; set; }

        public float Height { get; set; }
        public float Width { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int? OrderId { get; set; }

        public List<String> Images { get; set; } = [];

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

            var base64Images = framedArtwork.Images != null && framedArtwork.Images.Any()
                                    ? framedArtwork.Images.Select(x => ResizeAndConvertImage(x)).ToList()
                                    : [];
            

            return new FramedArtworkDTO {
                Height = framedArtwork.Height,
                Width = framedArtwork.Width,
                GlassId = framedArtwork.GlassId ?? 0,
                FrameId = framedArtwork.FrameId ?? 0,
                BackgroundId = framedArtwork.BackgroundId ?? 0,
                PaperId = framedArtwork.PaperId ?? 0,
                Quantity = framedArtwork.Quantity ?? 1,
                Images = base64Images
            };
        }

        public static FramedArtworkDTO Create (FramedArtwork framedArtwork) {

            if (framedArtwork is null)
                return null;
            
            return new FramedArtworkDTO {
                Id = framedArtwork.Id,
                Height = framedArtwork.Height,
                Width = framedArtwork.Width,
                TotalPrice = framedArtwork.TotalPrice,
                OrderId = framedArtwork.OrderId,
                Glass = MaterialDTO.Create (framedArtwork.Glass),
                Frame = MaterialDTO.Create (framedArtwork.Frame),
                Background = MaterialDTO.Create (framedArtwork.Background),
                Paper = MaterialDTO.Create (framedArtwork.Paper),
                Quantity = framedArtwork.Quantity,
                Images = [.. framedArtwork.ImageFile.Split(",")]
            };
        }

        private static string ResizeAndConvertImage (IFormFile image) {
            using var memoryStream = new MemoryStream();

            image.CopyTo(memoryStream);
            memoryStream.Position = 0;

            using var imageSharp = Image.Load(memoryStream.ToArray());

            imageSharp.Mutate(x => x.Resize(new ResizeOptions {
                Size = new Size(600, 600),
                Mode = ResizeMode.Max 
            }));

            using var resultStream = new MemoryStream();
                
            imageSharp.SaveAsJpeg(resultStream);
            resultStream.Position = 0;
            
            var base64String = Convert.ToBase64String(resultStream.ToArray());
            return base64String;
        }
    }

}
