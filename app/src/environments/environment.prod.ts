import { IEnvironment } from './model';

export const environment: IEnvironment = {
  production: true,
  VERSION: require('../../package.json').version,
  API_URL: 'https://avaliadorpi-api.azurewebsites.net/api',
  AUTH_URL: 'https://avaliadorpi-identity.azurewebsites.net'
};
