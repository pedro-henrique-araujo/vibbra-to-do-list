import { environment } from './../src/environments/environment';
import axios from 'axios';

const api = axios.create({
  baseURL: environment.baseUrl,
});

export default api;
