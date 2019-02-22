import { environment } from '../../../environments/environment';
import { Injectable, EventEmitter, Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserManager, Log, MetadataService, User } from 'oidc-client';

const settings: any = {
  authority: environment.AUTH_URL,
  client_id: 'app',

  redirect_uri: document.location.origin + '/callback',
  post_logout_redirect_uri: document.location.origin + '/home',

  response_type: 'id_token token',
  scope: 'openid profile avaliadorpi offline_access',

  automaticSilentRenew: true,
  silent_redirect_uri: document.location.origin + '/silent',

  accessTokenExpiringNotificationTime: 4,
  filterProtocolClaims: true,
  loadUserInfo: true
};

@Injectable()
export class AuthService {
  private manager: UserManager = new UserManager(settings);
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

  completeSilentAuthentication(): Promise<void> {
    return this.manager.signinSilentCallback().then(() => {
      this.manager.getUser().then((user) => {
        this.user = user;
        console.log(this.user.access_token);
      });
    });
  }

  signOut(): Promise<void> {
    return this.manager.signoutRedirect();
  }
}
