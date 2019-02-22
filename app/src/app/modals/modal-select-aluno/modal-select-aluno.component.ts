import { Component, OnInit, SimpleChanges } from '@angular/core';

// tslint:disable-next-line:import-blacklist
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { ModalBaseComponent } from '../../core/components/_base/modal-base.component';
import { MessagesService } from '../../core/services/messages.service';
import { CriteriosService } from '../../services/criterios.service';
import { RequestErrorModel } from '../../core/models/request-error.model';
import { GruposService } from '../../services/grupos.service';
import { AlunoGrupoModel } from '../../shared/models/aluno-grupo.model';
import { AlunosService } from '../../services/alunos.service';
import { AlunoModel } from '../../shared/models/aluno.model';

@Component({
  selector: 'app-modal-select-aluno',
  templateUrl: './modal-select-aluno.component.html'
})
export class ModalSelectAlunoComponent extends ModalBaseComponent
  implements OnInit {
  public model = '';
  public alunos = [] as AlunoModel[];

  constructor(
    public _bsModalRef: BsModalRef,
    private gruposService: GruposService,
    private alunosService: AlunosService,
    private messagesService: MessagesService
  ) {
    super(_bsModalRef);
  }

  ngOnInit() {
    super.ngOnInit();
    this.initAlunos();
  }

  public onConfirm(): void {
    this.gruposService
      .addAluno(this.params, this.model)
      .then((dados) => {
        this.messagesService.save();
        this.return(true, dados);
      })
      .catch((data) => {
        this.messagesService.errors(data.error.errors as RequestErrorModel[]);
      });
  }

  private initAlunos() {
    this.alunosService.get().then((data) => (this.alunos = data));
  }
}
