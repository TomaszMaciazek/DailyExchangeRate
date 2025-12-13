using DailyExchangeRate.Domain.Entities;
using DailyExchangeRate.Domain.Models;
using DailyExchangeRate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyExchangeRate.Infrastructure.Repositories.Implementation
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly ExchangeRateDbContext _context;

        public ExchangeRateRepository(ExchangeRateDbContext context) => _context = context;

        public async Task<IEnumerable<ExchangeRateListItem>> GetCurrentExchangeRatesAsync()
        {
            return await _context.ExchangeRates
                .AsNoTracking()
                .Where(x => x.ReadingId == _context.ExchangeRateTableReadings
                    .OrderByDescending(r => r.Created)
                    .Select(r => r.Id)
                    .FirstOrDefault())
                .Select(x => new ExchangeRateListItem
                {
                    Code = x.Code,
                    Currency = x.Currency,
                    Mid = x.Mid
                })
                .ToListAsync();
        }

        public async Task AddExchangeRateTableReadingAsync(ExchangeRateTableReading reading)
        {
            _context.ExchangeRateTableReadings.Add(reading);
            await _context.SaveChangesAsync();
        }
    }
}
