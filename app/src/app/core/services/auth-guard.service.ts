import { Injectable, Component } from '@angular/core';
import { CanActivate } from '@angular/router';

import { AuthService } from './auth.service';

@Injectable()
export class AuthGuardService implements CanActivate {

    constructor(private auth: AuthService) {
    }

    canActivate(): Promise<boolean> {
        return this.auth.isLoggedIn().then(isLogged => {
            if (isLogged) {
                return true;
            }
            this.auth.startAuthentication();
            return false;
        });
    }
}