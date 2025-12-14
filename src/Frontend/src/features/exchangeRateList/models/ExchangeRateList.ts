import type { ExchangeRateListItem } from "./ExchangeRateListItem";

export interface ExchangeRateList{
    no: string;
    effectiveDate: string;
    rates: ExchangeRateListItem[];
}