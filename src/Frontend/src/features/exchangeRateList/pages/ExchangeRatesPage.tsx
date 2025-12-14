import { LoadingSpinner } from "../../../shared/components/LoadingSpinner";
import { pl } from "../../../shared/i18n";
import { ExchangeRatesHeader } from "../components/exchangeRatesHeader";
import { ExchangeRatesTable } from "../components/exchangeRatesTable";
import { useFetchExchangeRates } from "../hooks/useFetchExchangeRates";

export const ExchangeRatesPage = () => {
  const { data, loading, error } = useFetchExchangeRates();

  if (loading) return <LoadingSpinner />

  if (error) return (
    <div className="alert alert-danger text-center mt-1" role="alert">
      {error}
    </div>
  )

  if (!data) return (
    <div className="alert alert-info text-center mt-1" role="alert">
      {pl.noData}
    </div>
  )

  return (
    <>
      <ExchangeRatesHeader no={data.no} effectiveDate={data.effectiveDate} />
      <ExchangeRatesTable items={data.rates} />
    </>
  );
};
