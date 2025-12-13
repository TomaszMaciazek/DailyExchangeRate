using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Domain.Models;

namespace DailyExchangeRate.Application.Mapper.Implementation
{
    public class ExchangeRateListItemMapper : IExchangeRateListItemMapper
    {
        public IEnumerable<ExchangeRateListItemDto> Map(IEnumerable<ExchangeRateListItem> source) 
            => source.Select(Map);

        private ExchangeRateListItemDto Map(ExchangeRateListItem source)
        {
            return new ExchangeRateListItemDto
            {
                Code = source.Code,
                Currency = source.Currency,
                Mid = source.Mid
            };

        }
    }
}
