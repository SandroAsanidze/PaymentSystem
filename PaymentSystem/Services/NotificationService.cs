using Newtonsoft.Json;
using PaymentSystem.Interface;
using PaymentSystem.Models.Response;
using System.Text;

namespace PaymentSystem.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public NotificationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<bool> SendNotificationAsync(string url, object data)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
