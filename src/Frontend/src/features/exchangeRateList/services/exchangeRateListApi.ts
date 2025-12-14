
import { httpClient } from '../../../shared/services/httpClient';
import type { ExchangeRateList } from '../models/ExchangeRateList';

export const getCurrentExchangeRateList = async (): Promise<ExchangeRateList> => {
  const response = await httpClient.get('/exchangeRate');
  return response.data;
};