using DailyExchangeRate.Application.Integrations.Nbp;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace DailyExchangeRate.Infrastructure.Integrations.Nbp
{
    public sealed class NbpRatesClient(HttpClient httpClient) : INbpRatesClient
    {
        public async Task<IReadOnlyList<ExchangeRateTableData>> GetLatestTableAsync(
            CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetFromJsonAsync<List<NbpTableResponse>>(
                "exchangerates/tables/b?format=json",
                cancellationToken);

            if (response is null)
            {
                throw new InvalidDataException("NBP returned an empty or invalid response.");
            }

            return response.Select(table => new ExchangeRateTableData(
                table.No,
                table.EffectiveDate,
                table.Rates.Select(rate => new ExchangeRateData(
                    rate.Code,
                    rate.Currency,
                    rate.Mid)).ToList()))
                .ToList();
        }

        private sealed class NbpTableResponse
        {
            [JsonPropertyName("no")]
            public required string No { get; init; }

            [JsonPropertyName("effectiveDate")]
            public DateTime EffectiveDate { get; init; }

            [JsonPropertyName("rates")]
            public required List<NbpRateResponse> Rates { get; init; }
        }

        private sealed class NbpRateResponse
        {
            [JsonPropertyName("code")]
            public required string Code { get; init; }

            [JsonPropertyName("currency")]
            public required string Currency { get; init; }

            [JsonPropertyName("mid")]
            public decimal Mid { get; init; }
        }
    }



}
