using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Application.Services.Interfaces;
using DailyExchangeRate.Infrastructure.Repositories.Interfaces;

namespace DailyExchangeRate.Application.Services.Implementation
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateRepository _repository;
        private readonly IExchangeRateListItemMapper _mapper;

        public ExchangeRateService(
            IExchangeRateRepository repository,
            IExchangeRateListItemMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ExchangeRateListDto> GetCurrentExchangeRatesAsync()
            => _mapper.Map(await _repository.GetCurrentExchangeRatesAsync());
    }
}
