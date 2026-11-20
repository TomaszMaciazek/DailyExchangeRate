using DailyExchangeRate.Application.Integrations.Nbp;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Application.Repositories.Interfaces;
using DailyExchangeRate.Application.Services.Interfaces;

namespace DailyExchangeRate.Application.Services.Implementation
{
    public sealed class NbpImportService : INbpImportService
    {
        private readonly INbpRatesClient nbpRatesClient;
        private readonly IExchangeRateRepository repository;
        private readonly IExchangeRateTableReadingMapper mapper;

        public NbpImportService(INbpRatesClient nbpRatesClient, IExchangeRateRepository repository, IExchangeRateTableReadingMapper mapper)
        {
            this.nbpRatesClient = nbpRatesClient;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task ImportExchangeRatesAsync(CancellationToken cancellationToken = default)
        {
            var tables = await nbpRatesClient.GetLatestTableAsync(cancellationToken);

            foreach (var table in tables)
            {
                if (await repository.ExchangeRateTableReadingExistsAsync(table.No, cancellationToken))
                {
                    continue;
                }

                var reading = mapper.Map(table);
                await repository.AddExchangeRateTableReadingAsync(reading, cancellationToken);
            }
        }
    }
}
