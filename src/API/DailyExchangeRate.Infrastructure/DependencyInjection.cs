using DailyExchangeRate.Infrastructure.Repositories.Implementation;
using DailyExchangeRate.Infrastructure.Repositories.Interfaces;
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

            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            return services;
        }
    }
}
