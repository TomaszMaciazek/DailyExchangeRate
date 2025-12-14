import { useEffect, useState } from "react";
import type { ExchangeRateList } from "../models/ExchangeRateList";
import { getCurrentExchangeRateList } from "../services/exchangeRateListApi";
import { pl } from "../../../shared/i18n";

export const useFetchExchangeRates = () => {
  const [data, setData] = useState<ExchangeRateList | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);
        const result = await getCurrentExchangeRateList();
        setData(result);
      } catch (error) {
        setError(pl.loadingError);
      } finally {
        setLoading(false);
      }
    };
    
    fetchData();
  }, []);

  return { data, loading, error };
};
