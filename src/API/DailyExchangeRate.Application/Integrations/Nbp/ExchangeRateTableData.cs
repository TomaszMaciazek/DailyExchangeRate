namespace DailyExchangeRate.Application.Integrations.Nbp
{
    public sealed record ExchangeRateTableData(
        string No,
        DateTime EffectiveDate,
        IReadOnlyList<ExchangeRateData> Rates
    );
}
