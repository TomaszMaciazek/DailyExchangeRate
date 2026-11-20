using DailyExchangeRate.Domain.Entities;
using DailyExchangeRate.Domain.Models;

namespace DailyExchangeRate.Application.Repositories.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task AddExchangeRateTableReadingAsync(ExchangeRateTableReading reading, CancellationToken cancellationToken = default);
        Task<bool> ExchangeRateTableReadingExistsAsync(string no, CancellationToken cancellationToken = default);
        Task<ExchangeRateList> GetCurrentExchangeRatesAsync();
    }
}