import "./App.css";
import { pl } from "./shared/i18n";
import { ExchangeRatesPage } from "./features/exchangeRateList/pages/ExchangeRatesPage";

function App() {

  return (
    <>
      <nav className="navbar navbar-dark bg-dark py-3 px-5">
        <span className="navbar-brand">
          {pl.siteHeader}
        </span>
      </nav>
      <div className="min-vh-100">
        <main className="flex-grow-1 container-fluid bg-body p-0">
          <ExchangeRatesPage />
        </main>
      </div>
    </>
  );
}

export default App;
