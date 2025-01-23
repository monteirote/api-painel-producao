using api_painel_producao.Data;
using api_painel_producao.DTOs;
using api_painel_producao.Models;
using api_painel_producao.Models.Enums;
using api_painel_producao.Utils;
using Microsoft.EntityFrameworkCore;

namespace api_painel_producao.Repositories{

    public interface IMaterialRepository {
        Task CreateMaterial (MaterialDTO material);
        Task<MaterialDTO> GetMaterialById (int id);
        Task<List<MaterialDTO>> GetMaterialsByType (MaterialType type);
        Task DeleteMaterial (int id);
        Task<bool> CheckMaterialExistence (MaterialType type, int materialId);
    }

    public class MaterialRepository : IMaterialRepository {

        private readonly AppDbContext _context;

        public MaterialRepository (AppDbContext context) {
            _context = context;
        }

        public async Task CreateMaterial (MaterialDTO material) {

            var materialType = (MaterialType) Enum.Parse(typeof(MaterialType), material.Type);
            var materialMeasurementUnit = (MeasurementUnit) Enum.Parse(typeof(MeasurementUnit), material.MeasurementUnit);

            var materialToAdd = new Material {
                Name = material.Name,
                Description = material.Description,
                Type = materialType,
                MeasurementUnit = materialMeasurementUnit,
                ValueByUnit = material.ValueByUnit
            };

            _context.Materials.Add(materialToAdd);

            await _context.SaveChangesAsync();
        }

        public async Task<MaterialDTO> GetMaterialById (int id) {

            var materialFound = await _context.Materials.FirstOrDefaultAsync(x => x.Id == id);

            return MaterialDTO.Create(materialFound);
        }

        public async Task<List<MaterialDTO>> GetMaterialsByType (MaterialType type) {

            var results = await _context.Materials.Where(x => x.Type == type)
                                                    .Select(x => MaterialDTO.Create(x))
                                                    .ToListAsync();

            return results;
        }

        public async Task DeleteMaterial (int id) {

            var materialToDelete = await _context.Materials.FirstOrDefaultAsync(x => x.Id == id);

            if (materialToDelete is null) return;

            _context.Materials.Remove(materialToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckMaterialExistence (MaterialType type, int materialId) {

            var materialFound = await _context.Materials.FirstOrDefaultAsync(x => x.Id == materialId);

            if (materialFound is null) return false;

            if (materialFound.Type != type) return false;
            
            return true;
        }
    }
}
