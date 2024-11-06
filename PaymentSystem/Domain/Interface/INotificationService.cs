namespace PaymentSystem.Domain.Interface
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(string url, object data);
    }
}
