namespace api_painel_producao.Models.DTOs
{

    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public static UserDTO Create (User userData) {
            if (userData is null) return null;

            return new UserDTO {
                Id = userData.Id,
                Name = userData.Name,
                Email = userData.Email,
                Username = userData.Username,
                Role = userData.Role.ToString(),
                IsActive = userData.IsActive,
                CreatedAt = userData.CreatedAt,
            };
        }
    }

    public class SimplifiedUserDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public SimplifiedUserDTO(User userData)
        {
            Id = userData.Id;
            Name = userData.Name;
            Role = userData.Role.ToString();
        }
    }
}
