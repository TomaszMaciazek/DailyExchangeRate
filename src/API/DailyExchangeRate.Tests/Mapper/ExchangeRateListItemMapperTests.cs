using DailyExchangeRate.Application.Mapper.Implementation;
using DailyExchangeRate.Domain.Models;

namespace DailyExchangeRate.Tests.Mapper;

public class ExchangeRateListItemMapperTests
{
    private readonly ExchangeRateListItemMapper _sut = new();

    [Fact]
    public void Map_ShouldMapAllProperties()
    {
        var source = new ExchangeRateList
        {
            No = "A/001/A",
            EffectiveDate = new DateTime(2026, 11, 20),
            Rates =
            [
                new ExchangeRateListItem { Code = "USD", Currency = "dolar amerykański", Mid = 3.65m },
                new ExchangeRateListItem { Code = "EUR", Currency = "euro", Mid = 4.21m }
            ]
        };

        var result = _sut.Map(source);

        Assert.NotNull(result);
        Assert.Equal(source.No, result.No);
        Assert.Equal(source.EffectiveDate, result.EffectiveDate);

        var rates = result.Rates.ToList();
        Assert.Collection(rates,
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
    public void Map_WhenSourceIsNull_ShouldReturnNull()
    {
        var result = _sut.Map(null!);

        Assert.Null(result);
    }
}
