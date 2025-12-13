using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Domain.Models;

namespace DailyExchangeRate.Application.Mapper.Interfaces
{
    public interface IExchangeRateListItemMapper
    {
        IEnumerable<ExchangeRateListItemDto> Map(IEnumerable<ExchangeRateListItem> source);
    }
}