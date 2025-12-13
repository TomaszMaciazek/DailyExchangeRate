using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Domain.Entities;

namespace DailyExchangeRate.Application.Mapper.Implementation
{
    public class ExchangeRateTableReadingMapper : IExchangeRateTableReadingMapper
    {
        public ExchangeRateTableReading? Map(ExchangeRateTableReadingDto source)
        {
            if (source == null)
            {
                return null;
            }
            return new ExchangeRateTableReading
            {
                Id = 0,
                No = source.No,
                Created = DateTime.Now,
                EffectiveDate = source.EffectiveDate,
                Rates = source.Rates?.Select(Map).ToList() ?? new List<ExchangeRate>()
            };
        }

        private ExchangeRate? Map(ExchangeRateDto source)
        {
            if (source == null)
            {
                return null;
            }
            return new ExchangeRate
            {
                Id = 0,
                Currency = source.Currency,
                Code = source.Code,
                Mid = source.Mid
            };
        }
    }
}
