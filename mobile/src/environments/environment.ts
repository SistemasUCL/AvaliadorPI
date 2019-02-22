import { IEnvironment } from './model';
import { ClientSettings } from './abstracts/client-settings.dev';

export const environment: IEnvironment = {
  production: false,
  VERSION: '1.0.0',
  API_URL: 'https://localhost:44300/api',
  requestToken: 'pwa',
  clientSettings: ClientSettings
}; // require('../../package.json').version,
