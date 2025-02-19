using api_painel_producao.Models;

namespace api_painel_producao.DTOs {

    public class UserDTO {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }


        public UserDTO (User userData) {

            this.Id = userData.Id;

            this.Name = userData.Name;
            this.Email = userData.Email;
            this.Username = userData.Username;
            this.Role = userData.Role.ToString();

            this.IsActive = userData.IsActive;
            this.CreatedAt = userData.CreatedAt;
        }
    }

    public class SimplifiedUserDTO {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public SimplifiedUserDTO (User userData) {
            this.Id = userData.Id;
            this.Name = userData.Name;
            this.Role = userData.Role.ToString();
        }
    }
}
