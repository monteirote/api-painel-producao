using api_painel_producao.Repositories;
using api_painel_producao.Models;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;
using api_painel_producao.Models.Enums;
using api_painel_producao.DTOs;
using api_painel_producao.Models.RequestModels.Customer;
using api_painel_producao.Models.DTOs;
using api_painel_producao.Models.ResponseModels.Customer;

namespace api_painel_producao.Services {

    public interface ICustomerService {
        Task<ServiceResponse<int>> CreateCustomerAsync (string token, CustomerDataRequestModel newCustomer);
        Task<ServiceResponse<CustomerDTO>> GetCustomerById (int id);
        Task<ServiceResponse<CustomerDTO>> UpdateCustomerById (int customerId, CustomerDataRequestModel customerData, string token);
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

        public async Task<ServiceResponse<int>> CreateCustomerAsync (string token, CustomerDataRequestModel newCustomerData) {
            try {

                var userData = await _authService.ExtractTokenInfo(token);

                //var customerToAdd = CustomerDTO.Create(newCustomerData);

                //await _repository.CreateAsync(customerToAdd, userData.Id);

                //return ServiceResponse<int>.Ok(customerToAdd.Id, "Customer created successfully.");

                return null;


            } catch (Exception e) {
                return ServiceResponse<int>.Fail("Failed to create customer.");
            }
        }

        public async Task<ServiceResponse<List<CustomerResponseModel>>> FindAllCustomersAsync () { 
            try {

                List<CustomerDTO> customers = await _repository.FindAllCustomersAsync();

                var response = customers.Select(x => CustomerResponseModel.Create(x)).ToList();

                return ServiceResponse<List<CustomerResponseModel>>.Ok(response);

            } catch (Exception e) {
                return ServiceResponse<List<CustomerResponseModel>>.Fail("Action failed: internal error.");
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

        public async Task<ServiceResponse<CustomerDTO>> UpdateCustomerById (int customerId, CustomerDataRequestModel customerData, string token) { 
            try {

                var tokenData = await _authService.ExtractTokenInfo(token);

                if (customerId != tokenData.Id && tokenData.Role.ToString() != "Admin")
                    return ServiceResponse<CustomerDTO>.DenyPermission();


                var customerFound = await _repository.GetByIdAsync(customerId);

                if (customerFound is null)
                    return ServiceResponse<CustomerDTO>.Fail("Action failed: This customer does not exist.");

                //var customerDTO = CustomerDTO.Create(customerData);

                //await _repository.UpdateCustomer(customerDTO, tokenData.Id);

                //return ServiceResponse<CustomerDTO>.Ok();

                return null;
            } catch (Exception e) {
                return ServiceResponse<CustomerDTO>.Fail("Action failed: internal error.");
            }
        }

        public async Task<ServiceResponse<int>> DeleteCustomer (int customerId, string token) {
            try {
                var customerFound = await _repository.GetByIdAsync(customerId);

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
