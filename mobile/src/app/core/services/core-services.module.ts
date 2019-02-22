import { NgModule } from '@angular/core';

import { ModalModule } from 'ngx-bootstrap/modal';

import { BaseRequestService } from './base-request.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseHttpInterceptor } from '../settings/http-interceptor';
import { AuthService } from './auth.service';
import { AuthGuardService } from './auth-guard.service';
import { MessagesService } from './messages.service';
import { MessagesBaseService } from './messages-base.service';
import { ModalsService } from './modals.service';

@NgModule({
  imports: [ModalModule.forRoot()],
  providers: [
    BaseRequestService,
    AuthService,
    AuthGuardService,
    ModalsService,
    MessagesBaseService,
    MessagesService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseHttpInterceptor,
      multi: true
    }
  ]
})
export class CoreServicesModule {}
