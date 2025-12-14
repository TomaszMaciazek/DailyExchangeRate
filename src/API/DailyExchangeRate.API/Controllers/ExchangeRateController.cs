using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyExchangeRate.API.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to retrieving exchange rate information.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ILogger<ExchangeRateController> _logger;

        /// <summary>
        /// Initializes a new instance of the ExchangeRateController class with the specified exchange rate service and logger.
        /// </summary>
        /// <param name="exchangeRateService">The service used to retrieve and manage exchange rate data from database.</param>
        /// <param name="logger">The logger</param>
        public ExchangeRateController(IExchangeRateService exchangeRateService, ILogger<ExchangeRateController> logger)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the current exchange rates reading data for all currencies.
        /// </summary>
        /// <returns>A collection of the latest exchange rates with table no and effective date as <see cref="ExchangeRateListDto"/> object</returns>
        [HttpGet]
        [ProducesResponseType<ExchangeRateListDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCurrentExchangeRates()
        {
            try
            {
                return Ok(await _exchangeRateService.GetCurrentExchangeRatesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExchangeRateController_GetCurrentExchangeRates_Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
