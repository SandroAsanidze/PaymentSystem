using PaymentSystem.Models.Merchant;

namespace PaymentSystem.Interface
{
    public interface IMerchant
    {
        Task<Merchant> GetMerchant(int merchantId);
        Task<bool> UpdatePaymentStatus(int transactionId, int status);
    }
}
