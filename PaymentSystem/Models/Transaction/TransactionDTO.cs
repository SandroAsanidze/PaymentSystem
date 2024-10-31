using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Models.Transaction
{
    public class TransactionDTO
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public string TransactionType { get; set; }
        public int MerchantId { get; set; }
        public int MerchantTransactionId { get; set; }

        [ForeignKey("StatusId")]
        public TransactionStatus? TransactionStatus { get; set; }
        public string? UserId { get; set; }
        public string? Hash { get; set; }
    }
}
