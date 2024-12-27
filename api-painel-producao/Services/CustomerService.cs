using api_painel_producao.Repositories;
using api_painel_producao.Models;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;
using api_painel_producao.Models.Enums;
using api_painel_producao.DTOs;

namespace api_painel_producao.Services {

    public interface ICustomerService {
        Task<ServiceResponse<int>> CreateCustomerAsync (string token, CreateCustomerViewModel newCustomer);
        Task<ServiceResponse<Customer>> GetCustomerById (int id);
        Task<ServiceResponse<Customer>> UpdateCustomerById (int id, string token, UpdateCustomerViewModel newData);
        Task<ServiceResponse<int>> DeleteCustomer (int id, string token);
        Task<ServiceResponse<List<CustomerDTO>>> FindAllCustomersAsync ();
    }

    public class CustomerService : ICustomerService {

        private readonly ICustomerRepository _repository;
        private readonly IAuthService _authService;

        public CustomerService (ICustomerRepository repository, IAuthService authService) {
            _repository = repository;
            _authService = authService;
        }


        public async Task<ServiceResponse<int>> CreateCustomerAsync (string token, CreateCustomerViewModel newCustomerData) {
            try {

                var userData = await _authService.ExtractTokenInfo(token);

                var customerToAdd = CustomerDTO.Create(newCustomerData);

                await _repository.CreateAsync(customerToAdd, userData.Id);

                return ServiceResponse<int>.Ok(customerToAdd.Id, "Customer created successfully.");


            } catch (Exception e) {
                return ServiceResponse<int>.Fail("Failed to create customer.");
            }
        }

        public async Task<ServiceResponse<List<CustomerDTO>>> FindAllCustomersAsync () { 
            try {

                var allCustomers = await _repository.FindAllCustomersAsync ();

                return ServiceResponse<List<CustomerDTO>>.Ok(customersFound);

            } catch (Exception e) {
                ServiceResponse<List<CustomerDTO>>.Fail("Action failed: internal error.");
            }
        }

        public async Task<ServiceResponse<CustomerDTO>> GetCustomerById (int id) { 
            try {

                var customerFound = await _repository.GetByIdAsync(id);

                if (customerFound is null)
                    return ServiceResponse<CustomerDTO>.Fail("Action failed: This customer does not exist.");

                return ServiceResponse<CustomerDTO>.Ok(customerFound);

            } catch (Exception e) {
                return ServiceResponse<CustomerDTO>.Fail("Action failed: internal error.");
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
                return ServiceResponse<Customer>.Fail("Action failed: internal error.");
            }
        }

        public async Task<ServiceResponse<CustomerDTO>> UpdateCustomerById (int customerId, UpdateCustomerViewModel customerData, string token) { 
            try {

                var tokenData = await _authService.ExtractTokenInfo(token);

                if (customerId != tokenData.Id && tokenData.Role != "Admin")
                    return ServiceResponse<CustomerDTO>.DenyPermission();


                var customerFound = await _repository.GetByIdAsync(customerId);

                if (customerFound is null)
                    return ServiceResponse<CustomerDTO>.Fail("Action failed: This customer does not exist.");




            } catch (Exception e) {
                return ServiceResponse<Customer>.Fail("Action failed: internal error.");
            }
        }

        public async Task<ServiceResponse<int>> DeleteCustomer (int customerId, string token) {
            try {
                Customer customerFound = await _repository.GetByIdAsync(customerId);

                var userInfo = await _authService.ExtractTokenInfo(token);

                if (customerFound is null)
                    return ServiceResponse<int>.Fail("Action failed: This customer does not exist.");

                if (!customerFound.IsActive)
                    return ServiceResponse<int>.Fail("Action failed: This customer is not active.");


                await _repository.DeactivateCustomer(customerId, userInfo.Id);

                return ServiceResponse<int>.Ok();

            } catch {
                return ServiceResponse<int>.Fail("Action failed: internal error.");
            }
        }

    }
}
