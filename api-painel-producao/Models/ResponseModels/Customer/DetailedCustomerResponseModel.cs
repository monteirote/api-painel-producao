using api_painel_producao.DTOs;
using api_painel_producao.Models.ResponseModels.Login;
using api_painel_producao.Models.ResponseModels.Order;

namespace api_painel_producao.Models.ResponseModels.Customer {
    public class DetailedCustomerResponseModel {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public UserResponseModel? DeactivatedBy { get; set; }
        public DateTime? DeactivatedAt { get; set; }

        public UserResponseModel CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public UserResponseModel? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public List<OrderResponseModel> Orders { get; set; } = [];

        public static DetailedCustomerResponseModel Create (CustomerDTO customerData) { 
            if (customerData is null) return null;

            return new DetailedCustomerResponseModel { 
                Id = customerData.Id,
                Name = customerData.Name,
                Email = customerData.Email,
                IsActive = customerData.IsActive,
                DeactivatedBy = UserResponseModel.Create(customerData.DeactivatedBy),
                DeactivatedAt = customerData.DeactivatedAt,
                CreatedBy = UserResponseModel.Create(customerData.CreatedBy),
                CreatedAt = customerData.CreatedAt,
                LastModifiedBy = UserResponseModel.Create(customerData.LastModifiedBy),
                LastModifiedAt = customerData.LastModifiedAt,
                Orders = customerData.Orders.Select(x => OrderResponseModel.Create(x)).ToList(),
            };
        }
    }
}
