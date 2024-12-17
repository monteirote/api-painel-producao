using api_painel_producao.Data;
using api_painel_producao.Models;
using Microsoft.EntityFrameworkCore;

namespace api_painel_producao.Repositories {

    public interface IUserRepository {
        Task CreateAsync (User user);
        Task UpdateAsync (User user);
        Task DeleteAsync (User user);
        Task DeactivateUserAsync (int userId, int tokenId);
        Task ActivateUserAsync (int userId, int tokenId);
        Task UpdatePassword (int userId, string[] passwordInfo, int tokenId);
        Task<List<User>> GetAllAsync ();
        Task<User?> GetByIdAsync (int id);
        Task<User?> FindUserByUsernameAsync (string username);
        Task<User?> FindUserByEmailAsync (string email);
        Task<List<User>> RetrieveUsersPendingApproval ();
    }

    public class UserRepository : IUserRepository {

        private readonly AppDbContext _context;

        public UserRepository (AppDbContext context) {
            _context = context;
        }

        public async Task<User?> GetByIdAsync (int id) {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> FindUserByUsernameAsync (string username) {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User?> FindUserByEmailAsync (string email) {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
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

        public async Task DeactivateUserAsync(int userId, int tokenId) {
            var userToDeactivate = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var userFromToken = await _context.Users.FirstOrDefaultAsync(x => x.Id == tokenId);

            userToDeactivate.IsActive = false;
            userToDeactivate.StatusLastModifiedBy = userFromToken;
            userToDeactivate.StatusLastModifiedById = userFromToken.Id;
            userToDeactivate.StatusLastModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePassword(int userId, string[] passwordInfo, int tokenId) { 

            var userToUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var userFromToken = await _context.Users.FirstOrDefaultAsync(x => x.Id == tokenId);

            userToUpdate.PasswordSalt = passwordInfo[0];
            userToUpdate.PasswordHash = passwordInfo[1];

            userToUpdate.DataLastModifiedBy = userFromToken;
            userToUpdate.DataLastModifiedById = tokenId;
            userToUpdate.DataLastModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task ActivateUserAsync (int userId, int tokenId) {
            var userToActivate = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var userFromToken = await _context.Users.FirstOrDefaultAsync(x => x.Id == tokenId);

            userToActivate.IsActive = true;
            userToActivate.StatusLastModifiedBy = userFromToken;
            userToActivate.StatusLastModifiedById = userFromToken.Id;
            userToActivate.StatusLastModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> RetrieveUsersPendingApproval () {
            var usersRetrieved = await _context.Users.Where(x => x.IsActive == false && x.StatusLastModifiedAt == null).ToListAsync();

            return usersRetrieved;
        }

    }
}
