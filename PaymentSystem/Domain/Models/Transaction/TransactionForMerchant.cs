namespace PaymentSystem.Domain.Models.Transaction
{
    public class TransactionForMerchant
    {
        public int TransactionId { get; set; }
        public int Amount { get; set; }
        public int MerchantId { get; set; }
        public string? UserId { get; set; }
    }
}
