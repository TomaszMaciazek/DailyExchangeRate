using DailyExchangeRate.Application.Integrations.Nbp;
using DailyExchangeRate.Application.Mapper.Interfaces;
using DailyExchangeRate.Application.Repositories.Interfaces;
using DailyExchangeRate.Application.Services.Implementation;
using DailyExchangeRate.Domain.Entities;
using Moq;

namespace DailyExchangeRate.Tests.Services;

public class NbpImportServiceTests
{
    [Fact]
    public async Task ImportExchangeRatesAsync_ShouldSkipExistingTables()
    {
        var existing = new ExchangeRateTableData("A/001/A", new DateTime(2026, 11, 20), []);
        var cancellationToken = new CancellationTokenSource().Token;

        var client = new Mock<INbpRatesClient>();
        client.Setup(x => x.GetLatestTableAsync(cancellationToken))
            .ReturnsAsync([existing]);

        var repository = new Mock<IExchangeRateRepository>();
        repository.Setup(x => x.ExchangeRateTableReadingExistsAsync(existing.No, cancellationToken))
            .ReturnsAsync(true);

        var mapper = new Mock<IExchangeRateTableReadingMapper>();
        var sut = new NbpImportService(client.Object, repository.Object, mapper.Object);

        await sut.ImportExchangeRatesAsync(cancellationToken);

        repository.Verify(x => x.ExchangeRateTableReadingExistsAsync(existing.No, cancellationToken), Times.Once);
        mapper.Verify(x => x.Map(It.IsAny<ExchangeRateTableData>()), Times.Never);
        repository.Verify(x => x.AddExchangeRateTableReadingAsync(It.IsAny<ExchangeRateTableReading>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ImportExchangeRatesAsync_ShouldMapAndAddNewTables()
    {
        var newTable = new ExchangeRateTableData(
            "A/002/A",
            new DateTime(2026, 11, 21),
            [new ExchangeRateData("USD", "dolar amerykański", 3.70m)]);
        var mapped = new ExchangeRateTableReading
        {
            No = newTable.No,
            EffectiveDate = newTable.EffectiveDate,
            Created = DateTime.UtcNow
        };

        var client = new Mock<INbpRatesClient>();
        client.Setup(x => x.GetLatestTableAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([newTable]);

        var repository = new Mock<IExchangeRateRepository>();
        repository.Setup(x => x.ExchangeRateTableReadingExistsAsync(newTable.No, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var mapper = new Mock<IExchangeRateTableReadingMapper>();
        mapper.Setup(x => x.Map(newTable)).Returns(mapped);

        var sut = new NbpImportService(client.Object, repository.Object, mapper.Object);

        await sut.ImportExchangeRatesAsync();

        mapper.Verify(x => x.Map(newTable), Times.Once);
        repository.Verify(x => x.AddExchangeRateTableReadingAsync(mapped, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ImportExchangeRatesAsync_ShouldProcessMultipleTablesIndependently()
    {
        var first = new ExchangeRateTableData("A/001/A", new DateTime(2026, 11, 20), []);
        var second = new ExchangeRateTableData("A/002/A", new DateTime(2026, 11, 21), []);

        var client = new Mock<INbpRatesClient>();
        client.Setup(x => x.GetLatestTableAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([first, second]);

        var repository = new Mock<IExchangeRateRepository>();
        repository.Setup(x => x.ExchangeRateTableReadingExistsAsync(first.No, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        repository.Setup(x => x.ExchangeRateTableReadingExistsAsync(second.No, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var mapped = new ExchangeRateTableReading { No = second.No, EffectiveDate = second.EffectiveDate };
        var mapper = new Mock<IExchangeRateTableReadingMapper>();
        mapper.Setup(x => x.Map(second)).Returns(mapped);

        var sut = new NbpImportService(client.Object, repository.Object, mapper.Object);

        await sut.ImportExchangeRatesAsync();

        mapper.Verify(x => x.Map(first), Times.Never);
        mapper.Verify(x => x.Map(second), Times.Once);
        repository.Verify(x => x.AddExchangeRateTableReadingAsync(mapped, It.IsAny<CancellationToken>()), Times.Once);
    }
}
