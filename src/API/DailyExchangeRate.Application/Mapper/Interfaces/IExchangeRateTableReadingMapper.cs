using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Domain.Entities;

namespace DailyExchangeRate.Application.Mapper.Interfaces
{
    public interface IExchangeRateTableReadingMapper
    {
        ExchangeRateTableReading Map(ExchangeRateTableReadingDto source);
    }
}