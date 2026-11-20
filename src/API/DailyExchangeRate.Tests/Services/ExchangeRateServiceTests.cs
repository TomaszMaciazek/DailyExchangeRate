using DailyExchangeRate.Application.Dto;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Application.Repositories.Interfaces;
using DailyExchangeRate.Application.Services.Implementation;
using DailyExchangeRate.Domain.Models;
using Moq;

namespace DailyExchangeRate.Tests.Services;

public class ExchangeRateServiceTests
{
    [Fact]
    public async Task GetCurrentExchangeRatesAsync_ShouldGetRepositoryDataAndMapIt()
    {
        var source = new ExchangeRateList
        {
            No = "A/001/A",
            EffectiveDate = new DateTime(2026, 11, 20),
            Rates = []
        };
        var expected = new ExchangeRateListDto
        {
            No = source.No,
            EffectiveDate = source.EffectiveDate,
            Rates = []
        };

        var repository = new Mock<IExchangeRateRepository>();
        repository.Setup(x => x.GetCurrentExchangeRatesAsync()).ReturnsAsync(source);

        var mapper = new Mock<IExchangeRateListItemMapper>();
        mapper.Setup(x => x.Map(source)).Returns(expected);

        var sut = new ExchangeRateService(repository.Object, mapper.Object);

        var result = await sut.GetCurrentExchangeRatesAsync();

        Assert.Same(expected, result);
        repository.Verify(x => x.GetCurrentExchangeRatesAsync(), Times.Once);
        mapper.Verify(x => x.Map(source), Times.Once);
    }
}
