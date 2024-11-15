namespace PaymentSystem.Domain.Models.Transaction
{
    public class TransactionForWithdraw
    {
        public int TransactionId { get; set; }
        public int Amount { get; set; }
        public int MerchantId { get; set; }
        public int UsersAccoutnumber { get; set; }
        public string? UsersFullName { get; set; }
        public string? MerchantUserId { get; set; }
    }
}
