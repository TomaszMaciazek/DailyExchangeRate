using DailyExchangeRate.Application.Integrations.Nbp;
using DailyExchangeRate.Domain.Entities;

namespace DailyExchangeRate.Application.Mapper.Interfaces
{
    public interface IExchangeRateTableReadingMapper
    {
        ExchangeRateTableReading Map(ExchangeRateTableData source);
    }
}