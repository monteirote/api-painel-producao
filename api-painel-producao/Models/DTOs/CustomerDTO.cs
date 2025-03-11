using api_painel_producao.Models;
using api_painel_producao.Models.DTOs;
using api_painel_producao.Models.RequestModels.Customer;

namespace api_painel_producao.DTOs
{

    public class CustomerDTO {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public List<OrderDTO> Orders { get; set; } = [];

        public bool IsActive { get; set; } = true;

        public UserDTO? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


        public UserDTO? DeactivatedBy { get; set; }
        public DateTime? DeactivatedAt { get; set; }


        public UserDTO? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }


        public static CustomerDTO Create (CustomerDataRequestModel customerData) {
            if (customerData is null) return null;

            return new CustomerDTO {
                Name = customerData.Name,
                Email = customerData.Email,
                PhoneNumber = customerData.PhoneNumber
            };
        }

        public static CustomerDTO Create (Customer customerData) {
            if (customerData is null) return null;

            return new CustomerDTO {
                Id = customerData.Id,
                Name = customerData.Name,
                Email = customerData.Email,
                PhoneNumber = customerData.PhoneNumber,

                Orders = customerData.Orders is null 
                                                ? new List<OrderDTO>()
                                                : customerData.Orders.Select(x => OrderDTO.Create(x)).ToList(),

                CreatedAt = customerData.CreatedAt,
                CreatedBy = UserDTO.Create(customerData.CreatedBy),

                DeactivatedAt = customerData.DeactivatedAt,
                DeactivatedBy = UserDTO.Create(customerData.DeactivatedBy),

                LastModifiedAt = customerData.LastModifiedAt,
                LastModifiedBy = UserDTO.Create(customerData.LastModifiedBy),
            };
        }     
    }
}
