using System.ComponentModel.DataAnnotations;

namespace api_painel_producao.Models.RequestModels.Login {
    public class ChangePasswordRequestModel {

        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "As senhas devem ter no mínimo 8 caracteres.")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "A senha antiga é obrigatória.")]
        [MinLength(8, ErrorMessage = "As senhas devem ter no mínimo 8 caracteres.")]
        public string OldPassword { get; set; }

    }
}
