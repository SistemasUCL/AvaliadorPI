import { Component, OnInit } from '@angular/core';
import { ModalBaseComponent } from '../../../../core/components/_base/modal-base.component';
import { BsModalRef } from 'ngx-bootstrap';
import { MessagesService } from '../../../../core/services/messages.service';
import { AvaliacaoModel } from '../../../../shared/models/avaliacao.model';
import { RequestErrorModel } from '../../../../core/models/request-error.model';
import { AlunosService } from '../../../../services/alunos.service';
import { AvaliacaoCriterioModel } from '../../../../shared/models/avaliacao-criterio.model';
import { AlunoAvaliacaoModel } from '../../../../shared/models/aluno-avaliacao.model';

@Component({
  selector: 'app-avaliacao-modal',
  templateUrl: './avaliacao-modal.component.html',
  styleUrls: ['./avaliacao-modal.component.scss']
})
export class AvaliacaoModalComponent extends ModalBaseComponent
  implements OnInit {
  public model = {} as AvaliacaoModel;

  public alunoAvaliacao = {} as AlunoAvaliacaoModel;

  constructor(
    public _bsModalRef: BsModalRef,
    private messagesService: MessagesService,
    private alunosService: AlunosService
  ) {
    super(_bsModalRef);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  public showModal() {
    this.model = this.params;

    this.initAvaliacao();

    super.showModal();
  }

  public onConfirm(): void {
    if (!this.validar()) {
      this.messagesService.showWarning('Existe(m) critério(s) sem avaliação');
      return;
    }

    this.alunosService
      .avaliar(this.model)
      .then((dados) => {
        this.messagesService.save();
        this.return(true, dados);
      })
      .catch((data) => {
        this.messagesService.errors(data.error.errors as RequestErrorModel[]);
      });
  }

  private initAvaliacao() {
    this.alunosService.getAvaliacao(this.model.alunoId).then((data) => {
      this.alunoAvaliacao = data;
      this.model.grupoId = data.grupoId;
      this.initModelCriterios();
    });
  }

  private initModelCriterios() {
    this.model.avaliacoesCriterios = [] as AvaliacaoCriterioModel[];

    this.alunoAvaliacao.criterios.forEach((x) => {
      this.model.avaliacoesCriterios.push({
        criterioId: x.id,
        nota: 0,
        peso: x.peso,
        id: ''
      });
    });
  }

  private validar(): boolean {
    return !this.model.avaliacoesCriterios.some((x) => !x.nota);
  }
}
