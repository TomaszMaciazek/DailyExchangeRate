using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Application.Services.Interfaces;
using DailyExchangeRate.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;

namespace DailyExchangeRate.Application.Services.Implementation
{
    public class NbpImportService : INbpImportService
    {
        private readonly HttpClient _http;
        private readonly IExchangeRateRepository _repository;
        private readonly IExchangeRateTableReadingMapper _mapper;

        public NbpImportService(IHttpClientFactory httpClientFactory, IExchangeRateRepository repository, IExchangeRateTableReadingMapper mapper)
        {
            _http = httpClientFactory.CreateClient("nbpClient");
            _mapper = mapper;
            _repository = repository;
        }

        public async Task ImportExchangeRatesAsync()
        {
            var response = await _http.GetAsync("exchangerates/tables/b?format=json");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<ExchangeRateTableReadingDto>>(content);
                var reading = _mapper.Map(result.FirstOrDefault());
                if (reading != null)
                {
                    await _repository.AddExchangeRateTableReadingAsync(reading);
                }
            }
            else
            {
                throw new Exception("Failed to fetch exchange rates from NBP.");
            }
        }
    }
}
