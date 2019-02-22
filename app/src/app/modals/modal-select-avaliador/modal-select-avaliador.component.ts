import { Component, OnInit, SimpleChanges } from '@angular/core';

// tslint:disable-next-line:import-blacklist
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { ModalBaseComponent } from '../../core/components/_base/modal-base.component';
import { MessagesService } from '../../core/services/messages.service';
import { CriteriosService } from '../../services/criterios.service';
import { RequestErrorModel } from '../../core/models/request-error.model';
import { AlunoGrupoModel } from '../../shared/models/aluno-grupo.model';
import { AvaliadorModel } from '../../shared/models/avaliador.model';
import { AvaliadoresService } from '../../services/avaliadores.service';
import { ProjetosService } from '../../services/projetos.service';

@Component({
  selector: 'app-modal-select-avaliador',
  templateUrl: './modal-select-avaliador.component.html'
})
export class ModalSelectAvaliadorComponent extends ModalBaseComponent
  implements OnInit {
  public model = '';
  public avaliadores = [] as AvaliadorModel[];

  constructor(
    public _bsModalRef: BsModalRef,
    private projetosService: ProjetosService,
    private avaliadoresService: AvaliadoresService,
    private messagesService: MessagesService
  ) {
    super(_bsModalRef);
  }

  ngOnInit() {
    super.ngOnInit();
    this.initAvaliadores();
  }

  public onConfirm(): void {
    this.projetosService
      .addAvaliador(this.params, this.model)
      .then((dados) => {
        this.messagesService.save();
        this.return(true, dados);
      })
      .catch((data) => {
        this.messagesService.errors(data.error.errors as RequestErrorModel[]);
      });
  }

  private initAvaliadores() {
    this.avaliadoresService.get().then((data) => (this.avaliadores = data));
  }
}
