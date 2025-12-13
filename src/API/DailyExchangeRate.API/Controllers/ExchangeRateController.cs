using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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

        public ExchangeRateController(IExchangeRateService exchangeRateService, ILogger<ExchangeRateController> logger)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the current exchange rates for all currencies.
        /// </summary>
        /// <returns>A collection of the latest exchange rates as <see cref="ExchangeRateListItemDto"/> objects</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<ExchangeRateListItemDto>>(StatusCodes.Status200OK)]
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
