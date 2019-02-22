// Modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastModule, ToastOptions } from 'ng2-toastr';

// Modules Application
import { PagesModule } from './pages/pages.module';
import { CoreLayoutsModule } from './core/layouts/core-layouts.module';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { ROUTES } from './app-routing.module';
import { CustomToastOption } from './core/settings/custom-toast-option';

import { AuthService } from './core/services/auth.service';
import { AuthGuardService } from './core/services/auth-guard.service';

@NgModule({
  declarations: [AppComponent],
  imports: [
    RouterModule.forRoot(ROUTES),
    BrowserAnimationsModule,
    BrowserModule,
    HttpClientModule,
    HttpModule,
    ToastModule.forRoot(),
    PagesModule,
    CoreLayoutsModule
  ],
  providers: [{ provide: ToastOptions, useClass: CustomToastOption }, AuthService, AuthGuardService],
  bootstrap: [AppComponent]
})
export class AppModule { }
