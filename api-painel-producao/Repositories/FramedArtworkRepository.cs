using Microsoft.EntityFrameworkCore;
using api_painel_producao.Models;
using api_painel_producao.DTOs;
using api_painel_producao.Data;

namespace api_painel_producao.Repositories {

    public interface IFramedArtworkRepository {
        Task<int> CreateFramedArtwork (FramedArtworkDTO framedArtwork);
        Task<FramedArtworkDTO> GetFramedArtorkById (int id);
        Task DeleteFramedArtwork (int id);
        Task AddFramedArtworkToOrder (FramedArtworkDTO framedArtwork, int orderId);
    }

    public class FramedArtworkRepository : IFramedArtworkRepository {

        private readonly AppDbContext _context;

        public FramedArtworkRepository (AppDbContext context) {
            _context = context;
        }

        public async Task<int> CreateFramedArtwork (FramedArtworkDTO framedArtwork) {

            var artworkModel = new FramedArtwork {
                Height = framedArtwork.Height,
                Width = framedArtwork.Width,
                TotalPrice = framedArtwork.TotalPrice,
                Frame = await _context.Materials.FirstOrDefaultAsync(x => x.Id == framedArtwork.FrameId),
                Glass = await _context.Materials.FirstOrDefaultAsync(x => x.Id == framedArtwork.GlassId),
                Background = await _context.Materials.FirstOrDefaultAsync(x => x.Id == framedArtwork.BackgroundId),
                Paper = await _context.Materials.FirstOrDefaultAsync(x => x.Id == framedArtwork.PaperId)
            };

            _context.FramedArtworks.Add(artworkModel);
            await _context.SaveChangesAsync();

            return artworkModel.Id;
        }

        public async Task<FramedArtworkDTO> GetFramedArtorkById (int id) { 

            var artworkFound = await _context.FramedArtworks.FirstOrDefaultAsync(x => x.Id == id);

            return FramedArtworkDTO.Create(artworkFound);
        }

        public async Task DeleteFramedArtwork (int id) {

            var artworkFound = await _context.FramedArtworks.FirstOrDefaultAsync(x => x.Id == id);

            _context.FramedArtworks.Remove(artworkFound);
            await _context.SaveChangesAsync();
        }

        public async Task AddFramedArtworkToOrder(FramedArtworkDTO framedArtworkDTO, int orderId) {

            int framedArtworkId = await CreateFramedArtwork(framedArtworkDTO);

            var framedArtwork = await _context.FramedArtworks.FirstOrDefaultAsync(x => x.Id == framedArtworkId);
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order is null || framedArtwork is null) return;

            framedArtwork.Order = order;
            framedArtwork.OrderId = order.Id;

            order.FramedArtworks.Add(framedArtwork);

            await _context.SaveChangesAsync();
        }

    }
}
