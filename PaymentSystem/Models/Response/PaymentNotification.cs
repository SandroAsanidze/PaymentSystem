namespace PaymentSystem.Models.Response
{
    public class PaymentNotification
    {
        public int TransactionID { get; set; }
        public string Status { get; set; }
        public int? Amount { get; set; }

    }
}
