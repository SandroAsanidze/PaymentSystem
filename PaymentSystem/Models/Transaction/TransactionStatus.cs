using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Models.Transaction
{
    public class TransactionStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
