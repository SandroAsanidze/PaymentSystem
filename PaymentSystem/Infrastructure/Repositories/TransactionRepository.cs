using Dapper;
using Microsoft.IdentityModel.Tokens;
using PaymentSystem.Domain.Interface;
using PaymentSystem.Domain.Models.Transaction;
using PaymentSystem.Infrastructure.Data;
using System.Data;

namespace PaymentSystem.Infrastructure.Repositories
{
    public class TransactionRepository : ITransaction
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> AddTransaction(TransactionForDeposit transaction, string type)
        {
            if (string.IsNullOrEmpty(transaction.MerchantUserId))
            {
                return 0;
            }

            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", transaction.MerchantUserId, DbType.String);
                parameters.Add("@Amount", transaction.Amount, DbType.Int32);
                parameters.Add("@TransactionType", type, DbType.String);
                parameters.Add("@StatusId", 4, DbType.Int32);
                parameters.Add("@TransactionDate", DateTime.Now, DbType.DateTime);
                parameters.Add("@MerchantId", transaction.MerchantId, DbType.String);
                parameters.Add("@TransactionId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "dbo.spTransaction_Insert",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@TransactionId");
            }
        }

        public async Task<int> AddTransaction(TransactionForWithdraw transaction, string type)
        {
            if (string.IsNullOrEmpty(transaction.MerchantUserId))
            {
                return 0;
            }

            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", transaction.MerchantUserId, DbType.String);
                parameters.Add("@Amount", transaction.Amount, DbType.Int32);
                parameters.Add("@TransactionType", type, DbType.String);
                parameters.Add("@StatusId", 4, DbType.Int32);
                parameters.Add("@TransactionDate", DateTime.Now, DbType.DateTime);
                parameters.Add("@MerchantId", transaction.MerchantId, DbType.String);
                parameters.Add("@TransactionId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "dbo.spTransaction_Insert",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@TransactionId");
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
                parameters.Add("@TransactionId", transactionId, DbType.Int32);

                var transactions = await connection.QueryFirstOrDefaultAsync<TransactionDTO>(
                    "dbo.spGetTransactionById",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return transactions!;
            }
        }

        public async Task<string> GetStatusName(int transactionId)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TransactionId", transactionId, DbType.Int32);

                var statusName = await connection.QueryFirstOrDefaultAsync<string>(
                    "dbo.spGetStatusName",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return statusName!;
            }
        }
    }
}
