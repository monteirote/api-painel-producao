using api_painel_producao.Models.DTOs;

namespace api_painel_producao.Models.ResponseModels.Login {
    public class UserResponseModel {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public static UserResponseModel Create (UserDTO userData) { 
            if (userData is null) return null;

            return new UserResponseModel {
                Id = userData.Id,
                Name = userData.Name,
                Email = userData.Email,
                Role = userData.Role,
            };
        }
    }
}
