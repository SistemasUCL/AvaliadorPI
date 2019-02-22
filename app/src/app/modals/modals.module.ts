import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { CoreModule } from '../core/core.module';
import { ModalModule } from 'ngx-bootstrap';
import { ServicesModule } from '../services/services.module';

import { ModalFormUsuarioComponent } from './modal-form-usuario/modal-form-usuario.component';
import { ModalFormCriterioComponent } from './modal-form-criterio/modal-form-criterio.component';
import { ModalSelectAlunoComponent } from './modal-select-aluno/modal-select-aluno.component';
import { ModalSelectAvaliadorComponent } from './modal-select-avaliador/modal-select-avaliador.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    ModalModule,
    ServicesModule,
    CoreModule
  ],
  exports: [
    ModalFormUsuarioComponent,
    ModalFormCriterioComponent,
    ModalSelectAlunoComponent,
    ModalSelectAvaliadorComponent
  ],
  declarations: [
    ModalFormUsuarioComponent,
    ModalFormCriterioComponent,
    ModalSelectAlunoComponent,
    ModalSelectAvaliadorComponent
  ],
  providers: [],
  entryComponents: [
    ModalFormUsuarioComponent,
    ModalFormCriterioComponent,
    ModalSelectAlunoComponent,
    ModalSelectAvaliadorComponent
  ]
})
export class ModalsModule {}
