import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { FormPageComponent } from '../_base/form-page.component';

import { MessagesService } from '../../core/services/messages.service';
import { ProjetosService } from '../../services/projetos.service';
import { ProfessoresService } from '../../services/professores.service';

import { ProjetoModel } from '../../shared/models/projeto.model';
import { ProfessorModel } from '../../shared/models/professor.model';
import { ToolbarButtonModel } from '../../core/models/toolbar-button.model';
import { DisciplinasService } from '../../services/disciplinas.service';
import { DisciplinaModel } from '../../shared/models/disciplina.model';
import { EstadoProjetoEnum } from '../../shared/enums/estado-projeto.enum';
import { RequestErrorModel } from '../../core/models/request-error.model';

@Component({
  selector: 'app-projeto',
  templateUrl: './projeto.component.html',
  styleUrls: ['./projeto.component.scss']
})
export class ProjetoComponent extends FormPageComponent<ProjetoModel>
  implements OnInit {
  public navOption = 'avaliacoes';
  public professores = [] as ProfessorModel[];
  public disciplinas = [] as DisciplinaModel[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected projetosService: ProjetosService,
    protected professoresService: ProfessoresService,
    protected disciplinasService: DisciplinasService,
    protected messagesService: MessagesService
  ) {
    super(route, projetosService, router, messagesService);
    this.routeBase = 'projetos';
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.initModel(params['id']).then(() => {
        this.initToolbar();
        this.addButtonsToolbar(this.initButtons());
      });
    });

    this.initProfessores();
    this.initDisciplinas();
  }

  public initProfessores() {
    this.professoresService.get().then((data) => (this.professores = data));
  }

  public initDisciplinas() {
    this.disciplinasService.get().then((data) => (this.disciplinas = data));
  }

  private initButtons(): ToolbarButtonModel[] {
    const list = [] as ToolbarButtonModel[];

    if (this.model.estado === EstadoProjetoEnum.Encerrado) {
      return list;
    }

    const display =
      this.model.estado === EstadoProjetoEnum.Elaboracao
        ? 'Liberar Avaliação'
        : 'Encerrar';

    const button = {
      display: display,
      icon: 'fa fa-save',
      action: () => {
        this.mudarEstado();
      },
      hidden: this.hideSave
    } as ToolbarButtonModel;

    list.push(button);
    return list;
  }

  public mudarEstado() {
    this.model.estado =
      this.model.estado === EstadoProjetoEnum.Elaboracao
        ? EstadoProjetoEnum.Avaliacao
        : EstadoProjetoEnum.Encerrado;

    this.projetosService
      .changeEstado(this.model.id, this.model.estado)
      .then(() => {
        this.messagesService.save();
        this.ngOnInit();
      })
      .catch((data) => {
        if (data.status && data.status === 404) {
          this.messagesService.errors();
          return;
        }

        this.messagesService.errors(data.error.errors as RequestErrorModel[]);
      });
  }
}
