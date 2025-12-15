import { pl } from '../i18n';

export const LoadingSpinner = () => {
  return (
    <div className="d-flex flex-column m-5 align-items-center justify-content-center align">
        <div className="spinner-border" role="status"></div>
        <p>{pl.loading}</p>
      </div>
    );
};