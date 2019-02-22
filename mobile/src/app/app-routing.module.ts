import { Routes } from '@angular/router';
import { FullLayoutComponent } from './core/layouts/full-layout/full-layout.component';
import { AvaliacaoComponent } from './pages/avaliacao/avaliacao.component';
import { HomeComponent } from './pages/home/home.component';
import { AuthCallbackComponent } from './pages/auth-callback/auth-callback.component';
import { AuthGuardService } from './core/services/auth-guard.service';
import { LeitorComponent } from './pages/leitor/leitor.component';

export const ROUTES: Routes = [
  {
    path: '',
    component: FullLayoutComponent,
    canActivate: [AuthGuardService],
    children: [
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'avaliacao/:id', component: AvaliacaoComponent }
    ]
  },
  {
    path: 'leitor',
    component: LeitorComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'callback',
    component: AuthCallbackComponent
  }
];
