import type { ExchangeRateListItem } from "./ExchangeRateListItem";

export interface ExchangeRateList{
    no: string;
    effectiveDate: Date;
    rates: ExchangeRateListItem[];
}