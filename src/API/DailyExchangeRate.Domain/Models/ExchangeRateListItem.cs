namespace DailyExchangeRate.Domain.Models
{
    public class ExchangeRateListItem
    {
        public string Code { get; set; }
        public string Currency { get; set; }
        public decimal Mid { get; set; }
    }
}
