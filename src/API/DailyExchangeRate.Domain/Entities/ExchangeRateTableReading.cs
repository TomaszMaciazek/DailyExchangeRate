using System.ComponentModel.DataAnnotations;

namespace DailyExchangeRate.Domain.Entities
{
    public class ExchangeRateTableReading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string No { get; set; }
        [Required]
        public DateTime EffectiveDate { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public ICollection<ExchangeRate> Rates { get; set; } = new List<ExchangeRate>();
    }
}
