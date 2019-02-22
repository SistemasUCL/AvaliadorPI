import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ServiceWorkerModule } from '@angular/service-worker';

import { ToastModule, ToastOptions } from 'ng2-toastr';

import { AppComponent } from './app.component';

import { environment } from '../environments/environment';
import { RouterModule } from '@angular/router';
import { ROUTES } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { LayoutsModule } from './core/layouts/layouts.module';
import { PagesModule } from './pages/pages.module';
import { CustomToastOption } from './core/settings/custom-toast-option';

@NgModule({
  declarations: [AppComponent],
  imports: [
    RouterModule.forRoot(ROUTES),
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    LayoutsModule,
    ToastModule.forRoot(),
    PagesModule,
    ServiceWorkerModule.register('/ngsw-worker.js', {
      enabled: environment.production
    })
  ],
  providers: [{ provide: ToastOptions, useClass: CustomToastOption }],
  bootstrap: [AppComponent]
})
export class AppModule {}
