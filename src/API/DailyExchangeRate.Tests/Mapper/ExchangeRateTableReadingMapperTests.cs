using DailyExchangeRate.Application.Integrations.Nbp;
using DailyExchangeRate.Application.Mapper.Implementation;

namespace DailyExchangeRate.Tests.Mapper;

public class ExchangeRateTableReadingMapperTests
{
    private readonly ExchangeRateTableReadingMapper _sut = new();

    [Fact]
    public void Map_ShouldMapTableAndRates()
    {
        var effectiveDate = new DateTime(2026, 11, 20);
        var source = new ExchangeRateTableData(
            "A/001/A",
            effectiveDate,
            [
                new ExchangeRateData("USD", "dolar amerykański", 3.65m),
                new ExchangeRateData("EUR", "euro", 4.21m)
            ]);

        var before = DateTime.UtcNow;
        var result = _sut.Map(source);
        var after = DateTime.UtcNow;

        Assert.Equal(source.No, result.No);
        Assert.Equal(source.EffectiveDate, result.EffectiveDate);
        Assert.True(result.Created >= before && result.Created <= after);
        Assert.Collection(result.Rates,
            rate =>
            {
                Assert.Equal("USD", rate.Code);
                Assert.Equal("dolar amerykański", rate.Currency);
                Assert.Equal(3.65m, rate.Mid);
            },
            rate =>
            {
                Assert.Equal("EUR", rate.Code);
                Assert.Equal("euro", rate.Currency);
                Assert.Equal(4.21m, rate.Mid);
            });
    }

    [Fact]
    public void Map_WhenSourceIsNull_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.Map(null!));
    }
}
