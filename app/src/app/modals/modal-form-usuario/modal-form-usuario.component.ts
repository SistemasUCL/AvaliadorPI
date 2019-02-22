import { Component, OnInit } from '@angular/core';

// tslint:disable-next-line:import-blacklist
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { ModalBaseComponent } from '../../core/components/_base/modal-base.component';
import { UsuariosService } from '../../services/usuarios.service';
import { UsuarioModel } from '../../shared/models/usuario.model';
import { MessagesService } from '../../core/services/messages.service';
import { RequestErrorModel } from '../../core/models/request-error.model';

@Component({
  selector: 'app-modal-form-usuario',
  templateUrl: './modal-form-usuario.component.html'
})
export class ModalFormUsuarioComponent extends ModalBaseComponent
  implements OnInit {
  constructor(
    public _bsModalRef: BsModalRef,
    private usuariosService: UsuariosService,
    private messagesService: MessagesService
  ) {
    super(_bsModalRef);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  public onConfirm(): void {
    this.usuariosService
      .save(this.model as UsuarioModel)
      .then((dados) => {
        this.messagesService.save();
        this.return(true, dados);
      })
      .catch((data) => {
        this.messagesService.errors(data.error.errors as RequestErrorModel[]);
      });
  }
}
