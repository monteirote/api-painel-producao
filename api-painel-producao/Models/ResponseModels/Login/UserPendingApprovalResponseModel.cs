using api_painel_producao.Models.DTOs;

namespace api_painel_producao.Models.ResponseModels.Login {
    public class UserPendingApprovalResponseModel {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


        public static UserPendingApprovalResponseModel Create (UserDTO userData) {
            if (userData == null)
                return null;

            return new UserPendingApprovalResponseModel {
                Id = userData.Id,
                Name = userData.Name,
                Email = userData.Email,
                Username = userData.Username,
                CreatedAt = userData.CreatedAt
            };
        }
    }
}
