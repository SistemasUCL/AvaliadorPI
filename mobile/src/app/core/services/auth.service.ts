import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { UserManager, User } from 'oidc-client';

@Injectable()
export class AuthService {
  private manager: UserManager = new UserManager(environment.clientSettings);
  private user: User = null;

  constructor() {
    this.manager.getUser().then((user) => {
      this.user = user;
    });
  }

  isLoggedIn(): Promise<boolean> {
    return this.manager.getUser().then((user) => {
      this.user = user;
      return user != null && !user.expired;
    });
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then((user) => {
      this.user = user;
    });
  }

  signOut(): Promise<void> {
    return this.manager.signoutRedirect();
  }
}
