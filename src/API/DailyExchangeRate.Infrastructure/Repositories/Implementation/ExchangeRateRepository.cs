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

        public async Task<ExchangeRateList?> GetCurrentExchangeRatesAsync() 
            => await _context.ExchangeRateTableReadings
                .Include(r => r.Rates)
                .OrderByDescending(r => r.EffectiveDate)
                .Select(x => new ExchangeRateList
                {
                    No = x.No,
                    EffectiveDate = x.EffectiveDate,
                    Rates = x.Rates.Select(r => new ExchangeRateListItem
                    {
                        Code = r.Code,
                        Currency = r.Currency,
                        Mid = r.Mid
                    }),
                })
                .FirstOrDefaultAsync();

        public async Task AddExchangeRateTableReadingAsync(ExchangeRateTableReading reading)
        {
            _context.ExchangeRateTableReadings.Add(reading);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExchangeRateTableReadingExistsAsync(string no)
        {
            return await _context.ExchangeRateTableReadings
                .AnyAsync(r => r.No == no);
        }
    }
}
