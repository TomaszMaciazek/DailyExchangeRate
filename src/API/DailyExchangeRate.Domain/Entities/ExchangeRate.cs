using System.ComponentModel.DataAnnotations;

namespace DailyExchangeRate.Domain.Entities
{
    public class ExchangeRate
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(3)]
        [Required]
        public string Code { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal Mid { get; set; }
        [Required]
        public int ReadingId { get; set; }
        public ExchangeRateTableReading Reading { get; set; }
    }
}
