using System.ComponentModel.DataAnnotations;

namespace api_painel_producao.Models.RequestModels.Customer {
    public class CustomerDataRequestModel {

        [Required(ErrorMessage = "The 'Name' field is required.")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "The 'Email' field is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The 'PhoneNumber' field is required.")]
        public string PhoneNumber { get; set; }
    }
}
