using PaymentSystem.Models.Deposit;
using PaymentSystem.Models.Merchant;
using PaymentSystem.Models.Transaction;
using PaymentSystem.Models.Withdraw;

namespace PaymentSystem.Interface
{
    public interface ITransaction
    {
        Task<int> AddTransaction(TransactionDTO transaction,string type);
        Task<IEnumerable<TransactionDTO>> GetPendingTransactions();
        Task<TransactionDTO> GetTransactionById(int transactionId);
    }
}
