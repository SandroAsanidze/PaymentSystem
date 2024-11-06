using PaymentSystem.Domain.Models.Merchant;

namespace PaymentSystem.Domain.Interface
{
    public interface IMerchant
    {
        Task<Merchant> GetMerchant(int merchantId);
        Task<bool> UpdatePaymentStatus(int transactionId, int status);
    }
}
