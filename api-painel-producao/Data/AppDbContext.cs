using Microsoft.EntityFrameworkCore;
using api_painel_producao.Models;

namespace api_painel_producao.Data {

    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
