import "./App.css";
import { ExchangeRatesPage } from "./features/exchangeRateList/pages/ExchangeRatesPage";

function App() {

  return (
    <>
      <div className="min-vh-100 d-flex flex-column">
        <main className="flex-grow-1 container-fluid bg-body py-3 px-5">
          <ExchangeRatesPage />
        </main>
      </div>
    </>
  );
}

export default App;
