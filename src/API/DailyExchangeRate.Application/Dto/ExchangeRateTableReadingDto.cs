namespace DailyExchangeRate.Application.Dto
{
    public class ExchangeRateTableReadingDto
    {
        public string Table { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public ICollection<ExchangeRateDto> Rates { get; set; }
    }
}
