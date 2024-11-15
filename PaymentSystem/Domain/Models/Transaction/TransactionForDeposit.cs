namespace PaymentSystem.Domain.Models.Transaction
{
    public class TransactionForDeposit
    {
        public int TransactionId { get; set; }
        public int Amount { get; set; }
        public int MerchantId { get; set; }
        public string? MerchantUserId { get; set; }
    }
}
