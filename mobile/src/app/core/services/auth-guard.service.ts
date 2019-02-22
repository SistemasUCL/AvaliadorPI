import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private authService: AuthService) { }

  canActivate(): Promise<boolean> {
    return this.authService.isLoggedIn().then(isLogged => {
      if (isLogged) {
        return true;
      }

      this.authService.startAuthentication();
      return false;
    });

  }

}
