import { Component } from '@angular/core';
import { AuthService } from '../../core/services/auth.service'
import { Router } from '@angular/router';

@Component({
    selector: 'callback',
    template: ''
})

export class CallbackComponent {
    constructor(private auth: AuthService,
        private router: Router) {
        auth.completeAuthentication().then(() => {
            this.router.navigateByUrl('home');
        });
    }
}