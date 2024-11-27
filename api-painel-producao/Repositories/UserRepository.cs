using api_painel_producao.Data;
using api_painel_producao.Models;
using Microsoft.EntityFrameworkCore;

namespace api_painel_producao.Repositories {

    public interface IUserRepository {
        Task CreateAsync (User user);
        Task UpdateAsync (User user);
        Task DeleteAsync (User user);
        Task DeactivateUserAsync (User user);
        Task UpdatePassword (User user, string salt, string hash);
        Task<List<User>> GetAllAsync ();
        Task<User?> GetByIdAsync (int id);
        Task<User?> GetByUsernameAsync (string username);
    }

    public class UserRepository : IUserRepository {

        private readonly AppDbContext _context;

        public UserRepository (AppDbContext context) {
            _context = context;
        }

        public async Task<User?> GetByIdAsync (int id) {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync (string username) {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<List<User>> GetAllAsync () {
            return await _context.Users.ToListAsync();
        }

        public async Task CreateAsync (User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync (User user) {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync (User user) {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateUserAsync (User user) {
            user.IsActive = false;
            user.DeactivatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePassword (User user, string salt, string hash) {
            user.PasswordSalt = salt;
            user.PasswordHash = hash;

            user.LastModifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task ActivateUserAsync (User user) {
            user.IsActive = true;
            user.LastModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
