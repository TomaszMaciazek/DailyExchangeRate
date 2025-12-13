using System.ComponentModel.DataAnnotations;

namespace DailyExchangeRate.Domain.Entities
{
    public class ExchangeRate
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(3)]
        public string Code { get; set; }
        public string Currency { get; set; }
        public decimal Mid { get; set; }
        public int ReadingId { get; set; }
        public ExchangeRateTableReading Reading { get; set; }
    }
}
