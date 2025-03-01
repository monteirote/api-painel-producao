using System.Linq;
using api_painel_producao.Data;
using api_painel_producao.Models;
using api_painel_producao.DTOs;
using Microsoft.EntityFrameworkCore;

namespace api_painel_producao.Repositories {

    public interface ICustomerRepository {
        Task CreateAsync (CustomerDTO customer, int tokenId);

        Task<CustomerDTO?> GetByIdAsync (int id);

        Task<List<CustomerDTO>> FindAllCustomersAsync();

        Task UpdateCustomer (CustomerDTO newData, int tokenId);
        Task DeactivateCustomer (int objectId, int userId);
    }

    public class CustomerRepository : ICustomerRepository {


        private readonly AppDbContext _context;
        public CustomerRepository (AppDbContext context) {
            _context = context;
        }



        public async Task CreateAsync (CustomerDTO newCostumerData, int tokenId) {

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == tokenId);

            var customer = new Customer {

                Name = newCostumerData.Name,
                Email = newCostumerData.Email,
                PhoneNumber = newCostumerData.PhoneNumber,

                IsActive = true,

                CreatedBy = user,
                CreatedById = user.Id

            };

            user.CreatedCustomers.Add(customer);

            _context.Customers.Add(customer);

            newCostumerData.Id = customer.Id;

            await _context.SaveChangesAsync();

        }

        public async Task<List<CustomerDTO>> FindAllCustomersAsync () {

            var customersFound = await _context.Customers
                                                .Include(x => x.Orders)
                                                .Include(x => x.CreatedBy)
                                                .Include(x => x.LastModifiedBy)
                                                .Include(x => x.DeactivatedBy)
                                                .Where(x => x.IsActive).ToListAsync();

            List<CustomerDTO> customers = (from c in customersFound 
                                           where c.IsActive == true
                                           select CustomerDTO.Create(c)).ToList();

            return customers;
        }

        public async Task<CustomerDTO?> GetByIdAsync (int id) {
            Customer customerFound = await _context.Customers
                                                    .Include(x => x.Orders)
                                                    .Include(x => x.CreatedBy)
                                                    .Include(x => x.LastModifiedBy)
                                                    .Include(x => x.DeactivatedBy)
                                                    .FirstOrDefaultAsync(x => x.Id == id);

            return CustomerDTO.Create(customerFound);
        }

        public async Task UpdateCustomer (CustomerDTO customerToUpdate, int tokenId) {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == tokenId);
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerToUpdate.Id);

            customer.Name = customerToUpdate.Name;
            customer.Email = customerToUpdate.Email;
            customer.PhoneNumber = customerToUpdate.PhoneNumber;

            customer.LastModifiedBy = user;
            customer.LastModifiedById = user.Id;
            customer.LastModifiedAt = DateTime.Now;

            user.ModifiedCustomers.Add(customer);

            await _context.SaveChangesAsync();

        }

        public async Task DeactivateCustomer (int customerId, int userId) {

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            Customer customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

            customer.IsActive = false;
            customer.DeactivatedBy = user;
            customer.DeactivatedById = user.Id;
            customer.DeactivatedAt = DateTime.Now;

            user.DeactivatedCustomers.Add(customer);

            await _context.SaveChangesAsync();
        }
    }
}
