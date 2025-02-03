using Microsoft.EntityFrameworkCore;
using api_painel_producao.Models;

namespace api_painel_producao.Data {

    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<FramedArtwork> FramedArtworks { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {

            //optionsBuilder.UseSqlite("Data Source=app.db");

            optionsBuilder.UseMySql("",
                            new MySqlServerVersion(new Version(5, 6, 26)));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // User
            //modelBuilder.Entity<User>()
            //    .HasIndex(u => u.Username)
            //    .IsUnique();

            //modelBuilder.Entity<User>()
            //    .HasIndex(u => u.Email)
            //    .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u1 => u1.DataLastModifiedBy)
                .WithMany(u2 => u2.ModifiedUsersData)
                .HasForeignKey(u1 => u1.DataLastModifiedById)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u1 => u1.StatusLastModifiedBy)
                .WithMany(u2 => u2.ModifiedUsersStatus)
                .HasForeignKey(u1 => u1.StatusLastModifiedById)
                .OnDelete(DeleteBehavior.SetNull);



            // Customer
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CreatedBy)
                .WithMany(u => u.CreatedCustomers)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.LastModifiedBy)
                .WithMany(u => u.ModifiedCustomers)
                .HasForeignKey(c => c.LastModifiedById)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.DeactivatedBy)
                .WithMany(u => u.DeactivatedCustomers)
                .HasForeignKey(c => c.DeactivatedById)
                .OnDelete(DeleteBehavior.SetNull);



            // Order
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.Reference)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CreatedBy)
                .WithMany(u => u.CreatedOrders)
                .HasForeignKey(o => o.CreatedById)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CreatedFor)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CreatedForId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.LastModifiedBy)
                .WithMany(u => u.ModifiedOrders)
                .HasForeignKey(o => o.LastModifiedById)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
               .HasOne(o => o.CanceledBy)
               .WithMany(u => u.CanceledOrders)
               .HasForeignKey(o => o.CanceledById)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.FramedArtworks)
                .WithOne(fa => fa.Order)
                .HasForeignKey(fa => fa.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
