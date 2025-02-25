using api_painel_producao.Models.Enums;
using api_painel_producao.Models.RequestModels.FramedArtwork;
using api_painel_producao.Validators;
using api_painel_producao.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace api_painel_producao.Models.RequestModels.Order {
    public class OrderDataRequestModel {

        [Required]
        public string Reference { get; set; } = string.Empty;


        [Required]
        [EnumValidation (typeof(Priority), ErrorMessage = "Action failed: the value of 'Priority' is not valid.")]
        public string Priority { get; set; } = string.Empty;


        [Required]
        public decimal TotalPrice { get; set; }


        [Required]
        public int CustomerId { get; set; }


        [Required]
        public List<FramedArtworkDataRequestModel> FramedArtworks { get; set; } = [];

    }
}
