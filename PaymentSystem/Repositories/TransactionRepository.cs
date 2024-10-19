using Dapper;
using PaymentSystem.Data;
using PaymentSystem.Interface;
using PaymentSystem.Models.Deposit;
using PaymentSystem.Models.Merchant;
using PaymentSystem.Models.Transaction;
using PaymentSystem.Models.Withdraw;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace PaymentSystem.Repositories
{
    public class TransactionRepository : ITransaction
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTransaction(TransactionDTO transaction,string type)
        {
            if (string.IsNullOrEmpty(transaction.UserId))
            {
                return false;
            }

            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", transaction.UserId, DbType.String);
                parameters.Add("@Amount", transaction.Amount, DbType.Int32);
                parameters.Add("@TransactionType", type, DbType.String);
                parameters.Add("@StatusId", 4, DbType.Int32);
                parameters.Add("@TransactionDate", DateTime.Now, DbType.DateTime);

                await connection.ExecuteAsync(
                    "dbo.spTransaction_Insert",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public async Task<IEnumerable<TransactionDTO>> GetPendingTransactions()
        {
            using (var connection = _context.CreateConnection())
            {
                var transactions = await connection.QueryAsync<TransactionDTO>(
                    "dbo.spGetPendingPayment",
                    null,
                    commandType: CommandType.StoredProcedure);

                return transactions;
            }
        }

        public async Task<TransactionDTO> GetTransactionById(int transactionId)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TransactionId",transactionId, DbType.Int32);

                var transactions = await connection.QueryFirstOrDefaultAsync<TransactionDTO>(
                    "dbo.spGetTransactionById",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return transactions!;
            }
        }
    }
}
