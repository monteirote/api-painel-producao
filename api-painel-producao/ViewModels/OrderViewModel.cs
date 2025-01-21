using api_painel_producao.Models.Enums;
using api_painel_producao.Validators;
using System.ComponentModel.DataAnnotations;

namespace api_painel_producao.ViewModels {

    public class OrderViewModel { }

    public class CreateOrderViewModel {

        [Required]
        public string Reference { get; set; } = string.Empty;


        [Required]
        [EnumValidation(typeof(Priority), ErrorMessage = "Action failed: the value of 'Priority' is not valid.")]
        public string Priority { get; set; } = string.Empty;


        [Required]
        public decimal TotalPrice { get; set; }


        [Required]
        public int CustomerId { get; set; }


        [Required]
        public List<CreateFramedArtworkViewModel> FramedArtworks { get; set; } = [];
    }
}
