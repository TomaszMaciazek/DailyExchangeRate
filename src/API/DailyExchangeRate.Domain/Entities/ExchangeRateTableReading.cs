using System.ComponentModel.DataAnnotations;

namespace DailyExchangeRate.Domain.Entities
{
    public class ExchangeRateTableReading
    {
        [Key]
        public int Id { get; set; }
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime Created { get; set; }
        public ICollection<ExchangeRate> Rates { get; set; }
    }
}
