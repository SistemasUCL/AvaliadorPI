import { Component, OnInit } from '@angular/core';
import { AvaliacaoModel } from '../../shared/models/avaliacao.model';
import { AvaliacaoNotaModel } from '../../shared/models/avaliacao-nota.model';
import { AvaliacaoRetornoModel } from '../../shared/models/avaliacao-retorno.model';
import { CriterioRetornoModel } from '../../shared/models/criterio-retorno.model';
import { ActivatedRoute, Router } from '@angular/router';
import { GruposService } from '../../services/grupos.service';
import { MessagesService } from '../../core/services/messages.service';
import { RequestErrorModel } from '../../core/models/request-error.model';
import { ModalsService } from '../../core/services/modals.service';
import { ModalFaltaComponent } from './modal-falta/modal-falta.component';
import { ModalSizeEnum } from '../../core/enums/modal-size.enum';

@Component({
  selector: 'app-avaliacao',
  templateUrl: './avaliacao.component.html',
  styleUrls: ['./avaliacao.component.scss']
})
export class AvaliacaoComponent implements OnInit {
  public disabledConcluir = true;
  public disabledNext = false;
  public disabledPrevius = true;
  public criteriosReady = false;
  public model: AvaliacaoModel;

  public ausentes = [] as string[];
  public criterioIndex = 0;

  constructor(
    protected route: ActivatedRoute,
    protected gruposService: GruposService,
    private router: Router,
    private messagesService: MessagesService,
    private modalsService: ModalsService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.initModel(params['id']);
    });
  }

  public rating(avaliacao: any) {
    const grupo = this.model['avaliados'].find(
      (x) => x.id === this.model.grupoId
    );

    const keys = Object.keys(this.model.criterios[0]['avaliacao']);
    keys.splice(keys.indexOf(grupo.id), 1);

    this.disabledConcluir = !this.model.criterios.every((x) => {
      let avaliado = x['avaliacao'][grupo.id].nota > 0;
      if (avaliado) {
        return avaliado;
      }

      avaliado = keys.every((y) => {
        return x['avaliacao'][y].nota > 0;
      });

      return avaliado;
    });
  }

  public next() {
    this.criterioIndex++;
    this.disabledPrevius = false;
    if (this.criterioIndex === this.model.criterios.length - 1) {
      this.disabledNext = true;
    } else {
      this.disabledNext = false;
    }
    // navigator
  }

  public previous() {
    this.criterioIndex--;
    this.disabledNext = false;
    if (this.criterioIndex > 0) {
      this.disabledPrevius = false;
    } else {
      this.disabledPrevius = true;
    }
  }

  public informarFalta() {
    const modal = this.modalsService.showModal(
      ModalFaltaComponent,
      { ausentes: this.ausentes, alunos: this.model.alunos },
      ModalSizeEnum.Big
    );

    modal.subscribe((response) => {
      if (response.isOk) {
        this.ausentes = response.data;
        this.changeAvaliados();
      }
    });
  }

  public concluir() {
    const keys = Object.keys(this.model.criterios[0]['avaliacao']);

    const criterios = this.model.criterios.map(
      (x) =>
        ({
          id: x.id,
          avaliacoes: keys.map(
            (y) =>
              ({
                avaliadoId: y,
                nota: x['avaliacao'][y].nota
              } as AvaliacaoNotaModel)
          )
        } as CriterioRetornoModel)
    );

    const retorno = {
      grupoId: this.model.grupoId,
      criterios: criterios
    } as AvaliacaoRetornoModel;

    this.gruposService
      .addAvaliacao(retorno.grupoId, retorno)
      .then((x) => {
        this.messagesService.showSuccess('Grupo avaliado com sucesso.');
        this.router.navigateByUrl('home');
      })
      .catch((data) => {
        this.messagesService.errors(
          data.error ? (data.error.errors as RequestErrorModel[]) : undefined
        );
      });
  }

  private initModel(id: string) {
    this.gruposService
      .getAvaliacao(id)
      .then((avaliacao) => {
        this.model = avaliacao;
        this.model.criterios.map(
          (x) => (x['avaliacao'] = this.createAvaliacao(x))
        );
        this.criteriosReady = true;
      })
      .catch((data) => {
        this.messagesService.errors(
          data.error ? (data.error.errors as RequestErrorModel[]) : undefined
        );
        this.router.navigateByUrl('home');
      });
  }

  private createAvaliacao(criterio: any): any {
    const avaliacao = {};

    this.model['avaliados'] = [
      { id: this.model.grupoId, nome: 'Avaliação do Grupo' },
      ...this.model.alunos
    ];

    this.model['avaliados'].forEach((x) => {
      avaliacao[x.id] = { avaliadoId: x.id, criterioId: criterio.id, nota: 0 };
    });
    return avaliacao;
  }

  private changeAvaliados() {
    const alunos = this.model.alunos.filter((x) =>
      this.ausentes.includes(x.id)
    );

    const alunosAvaliados = this.model.alunos.filter(
      (x) =>
        !this.model['avaliados'].map((y) => y.id).includes(x.id) &&
        !this.ausentes.includes(x.id)
    );

    this.model['avaliados'] = this.model['avaliados'].filter(
      (x) => !alunos.map((y) => y.id).includes(x.id)
    );

    if (this.ausentes.length) {
      this.model.criterios.forEach((x) => {
        this.ausentes.forEach((y) => {
          delete x['avaliacao'][y];
        });
      });
    }

    if (!alunosAvaliados.length) {
      return;
    }

    this.model['avaliados'] = [...this.model['avaliados'], ...alunosAvaliados];

    this.model.criterios.forEach((x) => {
      alunosAvaliados.forEach((y) => {
        x['avaliacao'][y.id] = { avaliadoId: y.id, criterioId: x.id, nota: 0 };
      });
    });
  }
}
