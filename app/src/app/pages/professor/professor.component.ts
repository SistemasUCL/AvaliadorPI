import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { FormPageComponent } from '../_base/form-page.component';

import { ProfessoresService } from '../../services/professores.service';
import { MessagesService } from '../../core/services/messages.service';
import { UsuariosService } from '../../services/usuarios.service';

import { ProfessorModel } from '../../shared/models/professor.model';
import { UsuarioModel } from '../../shared/models/usuario.model';
import { DisciplinasService } from '../../services/disciplinas.service';
import { DisciplinaModel } from '../../shared/models/disciplina.model';
import { ModalsService } from '../../core/services/modals.service';
import { ModalFormUsuarioComponent } from '../../modals/modal-form-usuario/modal-form-usuario.component';
import { ModalSizeEnum } from '../../core/enums/modal-size.enum';

@Component({
  selector: 'app-professor',
  templateUrl: './professor.component.html',
  styleUrls: ['./professor.component.scss']
})
export class ProfessorComponent extends FormPageComponent<ProfessorModel>
  implements OnInit {
  public usuarios = [] as UsuarioModel[];
  public disciplinas = [] as DisciplinaModel[];

  public usuario = new UsuarioModel();

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected professoresService: ProfessoresService,
    protected usuariosService: UsuariosService,
    protected disciplinasService: DisciplinasService,
    protected messagesService: MessagesService,
    protected modalsService: ModalsService
  ) {
    super(route, professoresService, router, messagesService);
    this.routeBase = 'professores';
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.getUsuarios(params['id']);

      this.initModel(params['id']).then(() => {
        this.initToolbar();
      });
    });
    this.getDisciplinas();
  }

  public async getUsuarios(id?: string) {
    await this.usuariosService.get().then((data) => (this.usuarios = data));
    this.selectUsuario(id);
  }

  public getDisciplinas() {
    this.disciplinasService.get().then((data) => (this.disciplinas = data));
  }

  public selectUsuario(usuarioId: string) {
    this.usuario = usuarioId
      ? this.usuarios.find((x) => x.id === usuarioId)
      : new UsuarioModel();
  }

  public showModal() {
    const modal = this.modalsService.showModal(
      ModalFormUsuarioComponent,
      {},
      ModalSizeEnum.Big
    );
    modal.subscribe((response) => {
      if (response.isOk) {
        this.usuarios = [...this.usuarios, response.data];
        this.model.usuarioId = response.data.id;
      }
    });
  }
}
