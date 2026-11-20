namespace DailyExchangeRate.Application.Integrations.Nbp
{
    public interface INbpRatesClient
    {
        Task<IReadOnlyList<ExchangeRateTableData>> GetLatestTableAsync(CancellationToken cancellationToken = default);
    }
}
