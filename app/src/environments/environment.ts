import { IEnvironment } from './model';

export const environment: IEnvironment = {
  production: false,
  VERSION: require('../../package.json').version,
  API_URL: 'https://localhost:44300/api',
  AUTH_URL: 'https://localhost:44301'
}; 
