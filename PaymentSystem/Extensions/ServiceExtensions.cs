using Microsoft.EntityFrameworkCore;
using PaymentSystem.Data;
using PaymentSystem.Interface;
using PaymentSystem.Repositories;
using PaymentSystem.Services;

namespace PaymentSystem.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureScope(this IServiceCollection services)
        {
            services.AddScoped<ITransaction,TransactionRepository>();
            services.AddScoped<IMerchant,MerchantRepository>();
            services.AddScoped<IHashValidation,HashRepository>();
            services.AddScoped<INotificationService,NotificationService>();
        }
    }
}
