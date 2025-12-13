using DailyExchangeRate.Application.Dto;

namespace DailyExchangeRate.Application.Services.Interfaces
{
    public interface IExchangeRateService
    {
        Task<IEnumerable<ExchangeRateListItemDto>> GetCurrentExchangeRatesAsync();
    }
}