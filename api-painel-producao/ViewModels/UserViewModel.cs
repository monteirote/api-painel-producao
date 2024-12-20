﻿using System.ComponentModel.DataAnnotations;

namespace api_painel_producao.ViewModels {

    public class UserSignupViewModel {

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome pode ter no máximo 50 caracteres.")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "O nome é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "O nome de usuário só pode conter letras e números.")]
        public string Username { get; set; } = string.Empty;


        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }


    public class UserLoginViewModel {

        [Required (ErrorMessage = "O nome de usuário é obrigatório.")]
        [RegularExpression (@"^[a-zA-Z0-9]*$", ErrorMessage = "O nome de usuário só pode conter letras e números.")]
        public string Username { get; set; } = string.Empty;


        [Required (ErrorMessage = "A senha é obrigatória.")]
        [MinLength (8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }


    public class UserChangePasswordViewModel {

        [Required (ErrorMessage = "A nova senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
    }


    public class PendingApprovalUserViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
