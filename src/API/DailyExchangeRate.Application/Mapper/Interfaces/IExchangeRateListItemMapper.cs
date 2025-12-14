using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Domain.Models;

namespace DailyExchangeRate.Application.Mapper.Interfaces
{
    public interface IExchangeRateListItemMapper
    {
        ExchangeRateListDto Map(ExchangeRateList source);
    }
}