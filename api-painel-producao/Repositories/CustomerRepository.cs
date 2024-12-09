using api_painel_producao.Data;
using api_painel_producao.Models;
using Microsoft.EntityFrameworkCore;

namespace api_painel_producao.Repositories {

    public interface ICustomerRepository { 
        Task CreateAsync (Customer customer);
        Task<Customer?> GetByIdAsync (int id);
        Task<Customer> UpdateCustomer (Customer newData);
    }

    public class CustomerRepository : ICustomerRepository {

        private readonly AppDbContext _context;

        public async Task CreateAsync (Customer customer) {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == customer.CreatedById);

            customer.CreatedBy = user;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetByIdAsync (int id) {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> UpdateCustomer (Customer updatedUser) {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updatedUser.LastModifiedById);

            updatedUser.LastModifiedBy = user;

            _context.Customers.Update(updatedUser);
            await _context.SaveChangesAsync();

            return updatedUser;
        }
    }
}
