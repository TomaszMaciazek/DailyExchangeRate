using DailyExchangeRate.Infrastructure.Integrations.Nbp;

namespace DailyExchangeRate.Tests.Integrations;

[Trait("Category", "Integration")]
public class NbpRatesClientIntegrationTests
{
    [Fact]
    public async Task GetLatestTableAsync_ShouldDownloadCurrentTableBFromNbp()
    {
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.nbp.pl/api/"),
            Timeout = TimeSpan.FromSeconds(30)
        };
        var client = new NbpRatesClient(httpClient);

        var tables = await client.GetLatestTableAsync();

        Assert.NotEmpty(tables);
        Assert.All(tables, table =>
        {
            Assert.False(string.IsNullOrWhiteSpace(table.No));
            Assert.NotEqual(default, table.EffectiveDate);
            Assert.NotEmpty(table.Rates);
            Assert.All(table.Rates, rate =>
            {
                Assert.False(string.IsNullOrWhiteSpace(rate.Code));
                Assert.False(string.IsNullOrWhiteSpace(rate.Currency));
                Assert.True(rate.Mid > 0);
            });
        });
    }
}
