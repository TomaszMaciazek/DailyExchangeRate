import { type FC } from "react";
import type { ExchangeRateListItem } from "../models/ExchangeRateListItem";
import { calculateRateMultiplier } from "../../../shared/utils/rateUtils";
import { pl } from '../../../shared/i18n';

interface IExchangeRateTableProps {
  items: ExchangeRateListItem[];
}

export const ExchangeRatesTable: FC<IExchangeRateTableProps> = ({ items }) => {
  return (
    <section className="row">
      <div className="col p-4">
        <div className="table-responsive">
          <table className="table table-striped table-hover table-secondary table-bordered">
            <thead className="table-dark">
              <tr>
                <th scope="col" className="text-center col-6">
                  {pl.currency}
                </th>
                <th scope="col" className="text-center col-3">
                  {pl.code}
                </th>
                <th scope="col" className="text-center col-3">
                  {pl.mid}
                </th>
              </tr>
            </thead>
            <tbody className="table-group-divider">
              {items &&
                items.length > 0 &&
                items.map((rate) => {
                  //calculate multiplayer in order to present values similary to the way used on NBP site
                  const multiplier = calculateRateMultiplier(rate.mid);
                  return (
                    <tr key={rate.code}>
                      <td scope="col" className="text-center col-6 py-3">
                        {rate.currency}
                      </td>
                      <td scope="col" className="text-center col-3 py-3">
                        {multiplier} {rate.code}
                      </td>
                      <td scope="col" className="text-center col-3 py-3">
                        {(rate.mid * multiplier).toFixed(4)}
                      </td>
                    </tr>
                  );
                })}
              {(!items || items.length == 0) && (
                <tr className="py-3">
                  <td colSpan={3} className="text-center">
                    <b>{pl.noData}</b>
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>
    </section>
  );
};