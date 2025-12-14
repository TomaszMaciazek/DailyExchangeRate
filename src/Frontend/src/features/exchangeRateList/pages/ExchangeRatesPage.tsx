import { ExchangeRatesHeader } from "../components/exchangeRatesHeader";
import { ExchangeRatesTable } from "../components/exchangeRatesTable";
import { useFetchExchangeRates } from "../hooks/useFetchExchangeRates";

export const ExchangeRatesPage = () => {
  const { data, loading, error } = useFetchExchangeRates();

  if (loading)
    return (
      <div className="d-flex flex-column m-4 align-items-center justify-content-center">
        <div className="spinner-border" role="status"></div>
        <p>Ładowanie danych...</p>
      </div>
    );

  if (error) return <p>{error}</p>;

  if (!data) return <p>Brak danych</p>;

  return (
    <>
      <ExchangeRatesHeader no={data.no} effectiveDate={data.effectiveDate} />
      <ExchangeRatesTable items={data.rates} />
    </>
  );
};
