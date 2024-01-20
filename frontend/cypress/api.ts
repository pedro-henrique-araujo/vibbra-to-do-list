import { environment } from './../src/environments/environment';
import axios from 'axios';

const api = axios.create({
  baseURL: environment.backendBaseUrl,
});

export default api;
