using api_painel_producao.DTOs;
using api_painel_producao.Models.DTOs;

namespace api_painel_producao.Models.ResponseModels.Customer { 
    public class CustomerResponseModel {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public bool IsActive { get; set; } = false;
        public DateTime? DeactivatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        public static CustomerResponseModel Create (CustomerDTO customerData) {

            if (customerData == null)
                return null;

            return new CustomerResponseModel {
                Id = customerData.Id,
                Name = customerData.Name,
                Email = customerData.Email,
                IsActive = customerData.IsActive,
                DeactivatedAt = customerData.DeactivatedAt,
                CreatedBy = customerData.CreatedBy.Name
            };
        }

    }
}
