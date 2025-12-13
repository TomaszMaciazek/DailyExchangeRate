using DailyExchangeRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyExchangeRate.Infrastructure
{
    public class ExchangeRateDbContext : DbContext
    {
        public ExchangeRateDbContext(DbContextOptions<ExchangeRateDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Currency).IsRequired();
                entity.Property(e => e.Code).IsRequired().HasMaxLength(3);
                entity.Property(e => e.Mid).IsRequired().HasColumnType("decimal(18,6)");
                entity.HasOne(e => e.Reading)
                      .WithMany(r => r.Rates)
                      .HasForeignKey(x => x.ReadingId)
                      .IsRequired();
            });

            modelBuilder.Entity<ExchangeRateTableReading>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.No).IsRequired();
            });
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<ExchangeRateTableReading> ExchangeRateTableReadings { get; set; }
    }
}
