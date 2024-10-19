using PaymentSystem.Interface;
using PaymentSystem.Models.Deposit;
using PaymentSystem.Models.Withdraw;
using System.Security.Cryptography;
using System.Text;

namespace PaymentSystem.Repositories
{
    public class HashRepository:IHashValidation
    {
        public bool ValidateHashDeposit(DepositDTO request, string secretKey)
        {
            var concatenatedString = $"{request.Amount}{request.MerchantID}{request.TransactionId}{secretKey}";
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(concatenatedString);
                var hash = sha256.ComputeHash(bytes);
                var calculatedHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return calculatedHash == request.Hash.ToLower();
            }
        }

        public bool ValidateHashWithdraw(WithdrawDTO request, string secretKey)
        {
            var concatenatedString = $"{request.Amount}{request.MerchantID}{request.TransactionId}{request.UsersAccoutnumber}{request.UsersFullName}{secretKey} ";
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(concatenatedString);
                var hash = sha256.ComputeHash(bytes);
                var calculatedHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return calculatedHash == request.Hash.ToLower();
            }
        }
    }
}
