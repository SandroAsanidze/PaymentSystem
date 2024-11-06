using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Domain.Models.Merchant
{
    public class Merchant
    {
        [Key]
        public int MechantId { get; set; }
        public string UserId { get; set; }
        public string URL { get; set; }
        public string SecretKey { get; set; }
    }
}
