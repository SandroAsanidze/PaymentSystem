using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Application.Services;
using PaymentSystem.Domain.Interface;
using PaymentSystem.Domain.Models;
using PaymentSystem.Infrastructure.Data;
using PaymentSystem.Infrastructure.Repositories;

namespace PaymentSystem.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureScope(this IServiceCollection services)
        {
            services.AddScoped<ITransaction, TransactionRepository>();
            services.AddScoped<IMerchant, MerchantRepository>();
            services.AddScoped<IHashValidation, HashRepository>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            });
        }
    }
}
