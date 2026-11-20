namespace DailyExchangeRate.Application.Integrations.Nbp
{
    public sealed record ExchangeRateData(string Code, string Currency, decimal Mid);
}
