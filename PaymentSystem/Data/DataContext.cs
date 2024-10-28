using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using PaymentSystem.Models.Merchant;
using PaymentSystem.Models.Transaction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PaymentSystem.Models;

namespace PaymentSystem.Data
{
    public class DataContext: IdentityDbContext<AppUser>
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) :base(options)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );

            base.OnModelCreating(modelBuilder);
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);


        public DbSet<TransactionDTO> Transactions { get; set; }
        public DbSet<TransactionStatus> TransactionStatus { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
    }
}
