import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap';

import { ModalConfirmComponent } from './modal-confirm/modal-confirm.component';
import { ModalBaseComponent } from './_base/modal-base.component';

@NgModule({
  imports: [BrowserModule, FormsModule, ModalModule.forRoot()],
  exports: [FormsModule, ModalBaseComponent, ModalConfirmComponent],
  declarations: [ModalBaseComponent, ModalConfirmComponent],
  entryComponents: [ModalConfirmComponent]
})
export class CoreComponentsModule {}
