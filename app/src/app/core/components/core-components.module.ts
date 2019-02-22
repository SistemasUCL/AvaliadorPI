import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap';

import { ModalConfirmComponent } from './modal-confirm/modal-confirm.component';
import { GridComponent } from './grid/grid.component';
import { ModalBaseComponent } from './_base/modal-base.component';
import { ToolbarComponent } from './toolbar/toolbar.component';

@NgModule({
  imports: [BrowserModule, FormsModule, ModalModule.forRoot()],
  exports: [
    FormsModule,
    ModalBaseComponent,
    ModalConfirmComponent,
    GridComponent,
    ToolbarComponent
  ],
  declarations: [
    ModalBaseComponent,
    ModalConfirmComponent,
    GridComponent,
    ToolbarComponent
  ],
  entryComponents: [ModalConfirmComponent]
})
export class CoreComponentsModule {}
