import axios from 'axios';
import { appConfig } from '../config/appConfig';

export const httpClient = axios.create({
  baseURL: appConfig.apiUrl,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  }
});