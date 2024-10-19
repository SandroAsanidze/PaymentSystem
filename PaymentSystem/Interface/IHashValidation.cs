using PaymentSystem.Models.Deposit;
using PaymentSystem.Models.Withdraw;

namespace PaymentSystem.Interface
{
    public interface IHashValidation
    {
        bool ValidateHashDeposit(DepositDTO request, string secretKey);
        bool ValidateHashWithdraw(WithdrawDTO request, string secretKey);
    }
}
