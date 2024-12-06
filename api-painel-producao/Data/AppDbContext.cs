using Microsoft.EntityFrameworkCore;
using api_painel_producao.Models;

namespace api_painel_producao.Data {

    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Customer>()
               .HasOne(c => c.CreatedBy)  // Relacionamento com o criador
               .WithMany(u => u.Customers)  // Um usuário pode ter vários clientes
               .HasForeignKey(c => c.CreatedById)  // A chave estrangeira
               .OnDelete(DeleteBehavior.SetNull);  // Ao deletar o usuário, setar o campo como null (opcional)

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.LastModifiedBy)  // Relacionamento com o último modificador
                .WithMany(u => u.Customers)
                .HasForeignKey(c => c.LastModifiedById)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
