using DailyExchangeRate.Domain.Entities;
using DailyExchangeRate.Domain.Models;

namespace DailyExchangeRate.Infrastructure.Repositories.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task AddExchangeRateTableReadingAsync(ExchangeRateTableReading reading);
        Task<bool> ExchangeRateTableReadingExistsAsync(string no);
        Task<ExchangeRateList?> GetCurrentExchangeRatesAsync();
    }
}