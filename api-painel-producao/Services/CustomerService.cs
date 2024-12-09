using api_painel_producao.Repositories;
using api_painel_producao.Models;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;
using api_painel_producao.Models.Enums;

namespace api_painel_producao.Services {

    public interface ICustomerService {
        Task<ServiceResponse<int>> CreateCustomerAsync (string token, CreateCustomerViewModel newCustomer);
        Task<ServiceResponse<Customer>> GetCustomerById (int id);
        Task<ServiceResponse<Customer>> UpdateCustomerById (int id, string token, UpdateCustomerViewModel newData);
    }

    public class CustomerService : ICustomerService {

        private readonly ICustomerRepository _repository;
        private readonly IAuthService _authService;

        public CustomerService (ICustomerRepository repository, IAuthService authService) {
            _repository = repository;
            _authService = authService;
        }

        public async Task<ServiceResponse<int>> CreateCustomerAsync (string token, CreateCustomerViewModel newCustomer) { 
            try {

                var userInfo = _authService.ExtractTokenInfo(token);

                var customerToAdd = new Customer {
                    Name = newCustomer.Name,
                    Email = newCustomer.Email,
                    PhoneNumber = newCustomer.PhoneNumber,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = userInfo.Id 
                };

                await _repository.CreateAsync(customerToAdd);

                return ServiceResponse<int>.Ok(customerToAdd.Id, "Customer created successfully.");

            } catch (Exception e) {
                return ServiceResponse<int>.Fail("Failed to create customer.");
            }
        }

        public async Task<ServiceResponse<Customer>> GetCustomerById (int id) { 
            try {

                var customerFound = await _repository.GetByIdAsync(id);

                if (customerFound is null)
                    return ServiceResponse<Customer>.Fail("Action failed: This customer does not exist.");

                return ServiceResponse<Customer>.Ok(customerFound);

            } catch (Exception e) {
                return ServiceResponse<Customer>.Fail("Failed to find customer.");
            }
        }

        public async Task<ServiceResponse<Customer>> UpdateCustomerById (int id, string token, UpdateCustomerViewModel newData) {  
            try {
                var userInfo = await _authService.ExtractTokenInfo(token);

                var customerFound = await _repository.GetByIdAsync(id);

                if (customerFound is null)
                    return ServiceResponse<Customer>.Fail("Action failed: This customer does not exist.");

                if (userInfo.Role != UserRole.Admin && userInfo.Id != customerFound.Id)
                    return ServiceResponse<Customer>.DenyPermission();

                customerFound.Name = newData.Name ?? customerFound.Name;
                customerFound.Email = newData.Email ?? customerFound.Email;
                customerFound.PhoneNumber = newData.PhoneNumber ?? customerFound.PhoneNumber;

                customerFound.LastModifiedById = userInfo.Id;
                customerFound.LastModifiedAt = DateTime.Now;

                await _repository.UpdateCustomer(customerFound);

                return ServiceResponse<Customer>.Ok(customerFound);

            } catch (Exception e) {
                return ServiceResponse<Customer>.Fail("Failed to update customer.");
            }
        }


    }
}
