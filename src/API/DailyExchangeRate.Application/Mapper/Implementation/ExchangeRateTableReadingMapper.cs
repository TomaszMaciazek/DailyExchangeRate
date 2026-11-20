using DailyExchangeRate.Application.Integrations.Nbp;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Domain.Entities;

namespace DailyExchangeRate.Application.Mapper.Implementation;

public sealed class ExchangeRateTableReadingMapper : IExchangeRateTableReadingMapper
{
    public ExchangeRateTableReading Map(ExchangeRateTableData source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return new ExchangeRateTableReading
        {
            No = source.No,
            EffectiveDate = source.EffectiveDate,
            Created = DateTime.UtcNow,
            Rates = source.Rates.Select(rate => new ExchangeRate
            {
                Code = rate.Code,
                Currency = rate.Currency,
                Mid = rate.Mid
            }).ToList()
        };
    }
}