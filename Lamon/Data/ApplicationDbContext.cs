using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lamon.Models;

namespace Lamon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("Roleclaims ", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");

            builder.Entity<OrderProduct>()
                .HasKey(t => new { t.OrderId, t.ProductId });

            builder.Entity<OrderProduct>()
                .HasOne(pt => pt.Order)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(pt => pt.OrderId);

            builder.Entity<OrderProduct>()
               .HasOne(pt => pt.Product)
               .WithMany(p => p.OrderProducts)
               .HasForeignKey(pt => pt.ProductId);
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderProduct> OrderProduct { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source=amol-rider;Initial Catalog=Lamon_DB;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
       
    }
}