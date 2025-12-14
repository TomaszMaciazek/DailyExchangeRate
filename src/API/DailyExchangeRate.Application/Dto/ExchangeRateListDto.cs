namespace DailyExchangeRate.Application.Dto
{
    public class ExchangeRateListDto
    {
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public IEnumerable<ExchangeRateListItemDto> Rates { get; set; }
    }
}
