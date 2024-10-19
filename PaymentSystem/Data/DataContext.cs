using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using PaymentSystem.Models.Merchant;
using PaymentSystem.Models.Transaction;

namespace PaymentSystem.Data
{
    public class DataContext:DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) :base(options)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);


        public DbSet<TransactionDTO> Transactions { get; set; }
        public DbSet<TransactionStatus> TransactionStatus { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
    }
}
