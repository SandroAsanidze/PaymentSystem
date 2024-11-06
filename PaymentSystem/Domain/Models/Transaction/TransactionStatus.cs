using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Domain.Models.Transaction
{
    public class TransactionStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
