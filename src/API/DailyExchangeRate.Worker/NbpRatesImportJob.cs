using DailyExchangeRate.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DailyExchangeRate.Worker
{
    public class NbpRatesImportJob : IJob
    {
        private readonly INbpImportService _nbpImportService;
        private readonly ILogger<NbpRatesImportJob> _logger;

        public NbpRatesImportJob(INbpImportService nbpImportService, ILogger<NbpRatesImportJob> logger)
        {
            _nbpImportService = nbpImportService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Starting NBP exchange rates import job.");
                await _nbpImportService.ImportExchangeRatesAsync();
                _logger.LogInformation("Finished NBP exchange rates import job successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during NBP exchange rates import job.");
                throw;
            }
        }
    }
}
