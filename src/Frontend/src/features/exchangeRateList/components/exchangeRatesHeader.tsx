import type { FC } from "react";
import { formatDate } from "../../../shared/utils/dateUtils";
import { pl } from '../../../shared/i18n';

interface IExchangeRatesHeaderProps {
  no: string;
  effectiveDate: string;
}

export const ExchangeRatesHeader: FC<IExchangeRatesHeaderProps> = ({ no, effectiveDate }) => {
    const parsedDate = formatDate(effectiveDate ? new Date(effectiveDate) : null);
    return (
      <section className="row">
        <div className="col bg-light py-4 px-5 d-flex flex-column gap-3">
          <h2 className="text-left">{pl.tableNoLabel} {no}</h2>
          <h6 className="text-left">{pl.effectiveDateLabel} {parsedDate}</h6>
        </div>
      </section>
    );
}