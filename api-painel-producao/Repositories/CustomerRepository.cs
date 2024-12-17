using api_painel_producao.Data;
using api_painel_producao.Models;
using Microsoft.EntityFrameworkCore;

namespace api_painel_producao.Repositories {

    public interface ICustomerRepository { 
        Task CreateAsync (Customer customer);
        Task<Customer?> GetByIdAsync (int id);
        Task UpdateCustomer (Customer newData);
        Task DeactivateCustomer (int objectId, int userId);
    }

    public class CustomerRepository : ICustomerRepository {

        private readonly AppDbContext _context;


        public async Task DeactivateCustomer (int objectId, int userId) {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            Customer customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == objectId);

            customer.IsActive = false;

            customer.DeactivatedBy = user;
            customer.DeactivatedById = user.Id;
            customer.DeactivatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }


        public async Task<Customer?> GetByIdAsync (int id) {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task CreateAsync (Customer customer) {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == customer.CreatedById);

            customer.CreatedBy = user;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateCustomer (Customer updatedUser) {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updatedUser.LastModifiedById);

            updatedUser.LastModifiedBy = user;

            _context.Customers.Update(updatedUser);
            await _context.SaveChangesAsync();
        }

    }
}
