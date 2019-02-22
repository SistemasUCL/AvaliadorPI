import { IClientSettings } from './abstracts/client-settings';

export interface IEnvironment {
  production: boolean;
  API_URL: string;
  VERSION: string;
  requestToken: string;
  clientSettings: IClientSettings;
}
