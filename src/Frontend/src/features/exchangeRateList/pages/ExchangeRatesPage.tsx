import { LoadingSpinner } from "../../../shared/components/LoadingSpinner";
import { pl } from "../../../shared/i18n";
import { ExchangeRatesHeader } from "../components/exchangeRatesHeader";
import { ExchangeRatesTable } from "../components/exchangeRatesTable";
import { useFetchExchangeRates } from "../hooks/useFetchExchangeRates";

export const ExchangeRatesPage = () => {
  const { data, loading, error } = useFetchExchangeRates();

  if (loading) return <LoadingSpinner />

  if (error) return <p>{error}</p>

  if (!data) return <p>{pl.noData}</p>
  return (
    <>
      <ExchangeRatesHeader no={data.no} effectiveDate={data.effectiveDate} />
      <ExchangeRatesTable items={data.rates} />
    </>
  );
};
