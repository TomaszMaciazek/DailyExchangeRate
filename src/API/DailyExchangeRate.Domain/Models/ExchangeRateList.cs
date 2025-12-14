namespace DailyExchangeRate.Domain.Models
{
    public class ExchangeRateList
    {
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public IEnumerable<ExchangeRateListItem> Rates { get; set; }
    }
}
