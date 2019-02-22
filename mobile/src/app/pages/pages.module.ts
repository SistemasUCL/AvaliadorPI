import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { ZXingScannerModule } from '@zxing/ngx-scanner';

import { AvaliacaoComponent } from './avaliacao/avaliacao.component';
import { ComponentsModule } from '../components/components.module';
import { HomeComponent } from './home/home.component';
import { ServicesModule } from '../services/services.module';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { LeitorComponent } from './leitor/leitor.component';
import { ModalFaltaComponent } from './avaliacao/modal-falta/modal-falta.component';

@NgModule({
  imports: [
    ComponentsModule,
    FormsModule,
    BrowserModule,
    ZXingScannerModule,
    ServicesModule
  ],
  declarations: [
    HomeComponent,
    AvaliacaoComponent,
    AuthCallbackComponent,
    LeitorComponent,
    ModalFaltaComponent,
    ModalFaltaComponent
  ],
  exports: [
    ComponentsModule,
    AvaliacaoComponent,
    HomeComponent,
    AuthCallbackComponent,
    LeitorComponent,
    ModalFaltaComponent
  ],
  entryComponents: [ModalFaltaComponent]
})
export class PagesModule {}
