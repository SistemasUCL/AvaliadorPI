import { IEnvironment } from './model';

export const environment: IEnvironment = {
  production: true,
  VERSION: require('../../package.json').version,
  API_URL: 'https://API_HOST/api',
  AUTH_URL: 'https://IDENTITY_HOST',
};
