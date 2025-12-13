namespace DailyExchangeRate.Application.Services.Interfaces
{
    public interface INbpImportService
    {
        Task ImportExchangeRatesAsync();
    }
}