import { NgModule } from '@angular/core';

import { ModalModule } from 'ngx-bootstrap/modal';
import { AuthService } from './auth.service';
import { BaseRequestService } from './base-request.service';
import { RequestService } from './request.service';
import { ModalsService } from './modals.service';
import { MessagesBaseService } from './messages-base.service';
import { MessagesService } from './messages.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseHttpInterceptor } from '../settings/http-interceptor';
import { GridService } from './grid.service';

@NgModule({
  imports: [ModalModule.forRoot()],
  providers: [
    AuthService,
    BaseRequestService,
    RequestService,
    ModalsService,
    MessagesBaseService,
    MessagesService,
    GridService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseHttpInterceptor,
      multi: true
    }
  ]
})
export class CoreServicesModule { }
