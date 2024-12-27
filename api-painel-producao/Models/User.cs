using api_painel_producao.Models.Enums;

namespace api_painel_producao.Models {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Vendedor;


        public string PasswordSalt { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public bool IsActive { get; set; } = false;
        public User? StatusLastModifiedBy { get; set; } = null;
        public int? StatusLastModifiedById { get; set; } = null;
        public DateTime? StatusLastModifiedAt { get; set; } = null;


        public User? DataLastModifiedBy { get; set; } = null;
        public int? DataLastModifiedById { get; set; } = null;
        public DateTime? DataLastModifiedAt { get; set; } = null;



        public List<Customer> CreatedCustomers { get; set; } = new List<Customer>();

        public List<Customer> ModifiedCustomers { get; set; } = new List<Customer>();

        public List<Customer> DeactivatedCustomers { get; set; } = new List<Customer>();


        public List<User> ModifiedUsersStatus { get; set; } = new List<User>();

        public List<User> ModifiedUsersData { get; set; } = new List<User>();


        public List<Order> CreatedOrders { get; set; } = new List<Order>();
        public List<Order> ModifiedOrders { get; set; } = new List<Order>();
        public List<Order> CanceledOrders { get; set; } = new List<Order>();
    }
}
