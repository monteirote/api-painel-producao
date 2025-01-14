using Microsoft.EntityFrameworkCore;
using api_painel_producao.Models;
using api_painel_producao.DTOs;

namespace api_painel_producao.Repositories {

    public interface IFramedArtworkRepository {
        Task CreateFramedArtworkDTO (FramedArtworkDTO framedArtwork);

        Task<bool>
    }

    public class FramedArtworkRepository : IFramedArtworkRepository {

        private readonly AppDbContext _context;

        public FramedArtworkRepository (AppDbContext context) {
            _context = context;
        }

        public async Task CreateFramedArtworkDTO (FramedArtworkDTO framedArtwork) {

        }

    }
}
