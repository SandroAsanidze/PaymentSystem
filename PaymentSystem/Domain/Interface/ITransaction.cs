using PaymentSystem.Domain.Models.Transaction;

namespace PaymentSystem.Domain.Interface
{
    public interface ITransaction
    {
        Task<int> AddTransaction(TransactionForMerchant transaction, string type);
        Task<IEnumerable<TransactionDTO>> GetPendingTransactions();
        Task<TransactionDTO> GetTransactionById(int transactionId);
        Task<string> GetStatusName(int transactionId);
    }
}
