import { useState, type ChangeEvent, type FC } from "react";
import type { ExchangeRateListItem } from "../models/ExchangeRateListItem";
import { calculateRateMultiplier } from "../../../shared/utils/rateUtils";
import { pl } from "../../../shared/i18n";

interface IExchangeRateTableProps {
  items: ExchangeRateListItem[];
}

export const ExchangeRatesTable: FC<IExchangeRateTableProps> = ({ items }) => {
  const [query, setQuery] = useState<string>("");

  function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
    setQuery(event.target.value);
  }

  let searchQuery = query?.toLowerCase().trim() || "";
  let filteredItems =
    items?.filter(
      (item) =>
        query === "" ||
        item.code.toLowerCase().indexOf(searchQuery) !== -1 ||
        item.currency.toLowerCase().indexOf(searchQuery) !== -1
    ) || [];
  return (
    <>
      <div className="row py-2 mx-0">
        
        <div className="col-xs-12 col-sm-6 col-md-4 col-lg-3 col-xl-2 offset-lg-1 offset-xl-2 px-4 py-2">
          <input
            className="form-control"
            type="text"
            placeholder={pl.searchPlaceholder}
            onChange={handleInputChange}
          />
        </div>
      </div>
      <section className="row justify-content-center py-3 mx-0">
        <div className="col-xs-12 col-lg-10 col-xl-8 px-4 py-2">
          <div className="table-responsive">
            <table className="table table-striped table-hover table-secondary table-bordered">
              <thead className="table-dark">
                <tr>
                  <th scope="col" className="text-center col-6 py-3">
                    {pl.currency}
                  </th>
                  <th scope="col" className="text-center col-3 py-3">
                    {pl.code}
                  </th>
                  <th scope="col" className="text-center col-3 py-3">
                    {pl.mid}
                  </th>
                </tr>
              </thead>
              <tbody className="table-group-divider">
                {filteredItems.length > 0 &&
                  filteredItems.map((rate) => {
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
                {filteredItems.length === 0 && (
                  <tr className="py-3">
                    <td colSpan={3} className="text-center px-2 py-3">
                      <b>{pl.noItemsFound}</b>
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </section>
    </>
  );
};
