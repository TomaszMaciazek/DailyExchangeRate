import { pl } from '../i18n/pl';

export const LoadingSpinner = () => {
  return (
    <div className="d-flex flex-column m-4 align-items-center justify-content-center">
        <div className="spinner-border" role="status"></div>
        <p>{pl.loading}</p>
      </div>
    );
};