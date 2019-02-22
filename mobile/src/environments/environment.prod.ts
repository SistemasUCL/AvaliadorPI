import { IEnvironment } from './model';
import { ClientSettings } from './abstracts/client-settings.prod';

export const environment: IEnvironment = {
  production: true,
  VERSION: '1.0.0',
  API_URL: 'https://API_HOST/api',
  requestToken: 'pwa',
  clientSettings: ClientSettings
};
