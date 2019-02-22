import { Component } from '@angular/core';
import { AuthService } from '../../core/services/auth.service'
import { Router } from '@angular/router';

@Component({
    selector: 'silent',
    template: ''
})

export class SilentComponent {
    constructor(private auth: AuthService,
        private router: Router) {
        auth.completeSilentAuthentication();
    }
}