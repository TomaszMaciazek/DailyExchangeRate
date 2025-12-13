using DailyExchangeRate.Application.Mapper.Implementation;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Application.Services.Implementation;
using DailyExchangeRate.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyExchangeRate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiApplication(this IServiceCollection services)
        {
            services.AddScoped<IExchangeRateListItemMapper, ExchangeRateListItemMapper>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
            return services;
        }

        public static IServiceCollection AddWorkerApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(
                "nbpClient",
                client =>
                {
                    client.BaseAddress = new Uri(configuration["NbpApiUrl"]);
                    client.Timeout = TimeSpan.FromSeconds(30);
                }
            );
            services.AddScoped<IExchangeRateTableReadingMapper, ExchangeRateTableReadingMapper>();
            services.AddScoped<INbpImportService, NbpImportService>();
            return services;
        }
    }
}
