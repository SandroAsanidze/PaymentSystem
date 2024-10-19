using PaymentSystem.Models.Response;

namespace PaymentSystem.Interface
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(string url, object data);
    }
}
