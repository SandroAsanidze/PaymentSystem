using PaymentSystem.Models.Deposit;
using PaymentSystem.Models.Transaction;
using PaymentSystem.Models.Withdraw;

namespace PaymentSystem.Interface
{
    public interface IHashValidation
    {
        string Hash(string info);
        bool Verify(string infoHash,string hash);
    }
}
