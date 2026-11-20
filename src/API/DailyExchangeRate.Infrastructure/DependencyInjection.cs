using DailyExchangeRate.Application.Integrations.Nbp;
using DailyExchangeRate.Application.Repositories.Interfaces;
using DailyExchangeRate.Infrastructure.Integrations.Nbp;
using DailyExchangeRate.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyExchangeRate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExchangeRateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpClient<INbpRatesClient, NbpRatesClient>(client =>
            {
                var baseUrl = configuration["NbpApiUrl"]
                    ?? throw new InvalidOperationException("NbpApiUrl is not configured.");

                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

            return services;
        }
    }
}
