using api_painel_producao.Models;
using api_painel_producao.ViewModels;

namespace api_painel_producao.DTOs {

    public class CustomerDTO {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<SimplifiedOrderDTO> Orders { get; set; } = [];

        public bool IsActive { get; set; } = true;

        public UserDTO? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


        public UserDTO? DeactivatedBy { get; set; }
        public DateTime? DeactivatedAt { get; set; }


        public UserDTO? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }


        public static CustomerDTO? Create (Customer customerData) {
            return customerData is null ? null : new CustomerDTO (customerData);    
        }

        public static CustomerDTO Create (CreateCustomerViewModel newCostumerData) {
            return new CustomerDTO(newCostumerData);
        }

        public static CustomerDTO Create (UpdateCustomerViewModel customerData) {
            return new CustomerDTO(customerData);
        }

        private CustomerDTO (UpdateCustomerViewModel customerData) { 
            this.Name = customerData.Name;
            this.Email = customerData.Email;
            this.PhoneNumber = customerData.PhoneNumber;
        }

        private CustomerDTO (CreateCustomerViewModel newCustomerData) {
            this.Name = newCustomerData.Name;
            this.Email = newCustomerData.Email;
            this.PhoneNumber = newCustomerData.PhoneNumber;

            this.IsActive = true;

            this.CreatedAt = DateTime.Now;
        }
        
        public CustomerDTO (Customer customerData) {

            this.Id = customerData.Id;
            this.Email = customerData.Email;
            this.PhoneNumber = customerData.PhoneNumber;

            
            this.Orders = customerData.Orders.Select(x => new SimplifiedOrderDTO(x)).ToList();

            //this.Orders = (from o in customerData.Orders
            //               select new SimplifiedOrderDTO(o)).ToList();

            this.CreatedAt = customerData.CreatedAt;
            this.CreatedBy = customerData.CreatedBy == null ? null : new UserDTO (customerData.CreatedBy);

            this.DeactivatedAt = customerData.DeactivatedAt;
            this.DeactivatedBy = customerData.DeactivatedBy == null ? null : new UserDTO(customerData.DeactivatedBy);

            this.LastModifiedAt = customerData.LastModifiedAt;
            this.LastModifiedBy = customerData.LastModifiedBy == null ? null : new UserDTO(customerData.LastModifiedBy);
        }
    }

    public class SimplifiedCustomerDTO {

        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public SimplifiedCustomerDTO (Customer customerData) {
            this.Id = customerData.Id;
            this.Email = customerData.Email;
            this.PhoneNumber = customerData.PhoneNumber;
        }
    }
}
