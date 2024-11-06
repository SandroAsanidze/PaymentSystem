using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Domain.Models.Deposit
{
    public class DepositDTO
    {
        [Required]
        public int TransactionId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

        [Required]
        public int MerchantID { get; set; }

        [Required]
        public string Hash { get; set; }

    }
}
