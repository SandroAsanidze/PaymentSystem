using Dapper;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Data;
using PaymentSystem.Interface;
using PaymentSystem.Models.Merchant;
using System.Data;

namespace PaymentSystem.Repositories
{
    public class MerchantRepository:IMerchant
    {
        private readonly DataContext _context;
        public MerchantRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Merchant> GetMerchant(int merchantId)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MerchantId", merchantId, DbType.Int32);

                var merchant = await connection.QueryFirstOrDefaultAsync<Merchant>(
                    "dbo.spGetMerchant",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return merchant!;
            }
        }

        public async Task<bool> UpdatePaymentStatus(int transactionId, int status)
        {

            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TransactionId", transactionId, DbType.Int32);
                parameters.Add("@NewPaymentStatus", status, DbType.Int32);

                await connection.ExecuteAsync(
                    "dbo.UpdatePaymentStatus",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
