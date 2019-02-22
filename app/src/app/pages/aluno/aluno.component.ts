import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { FormPageComponent } from '../_base/form-page.component';
import { ModalFormUsuarioComponent } from '../../modals/modal-form-usuario/modal-form-usuario.component';

import { AlunosService } from '../../services/alunos.service';
import { UsuariosService } from '../../services/usuarios.service';
import { MessagesService } from '../../core/services/messages.service';
import { ModalsService } from '../../core/services/modals.service';

import { AlunoModel } from '../../shared/models/aluno.model';
import { UsuarioModel } from '../../shared/models/usuario.model';
import { ModalSizeEnum } from '../../core/enums/modal-size.enum';

@Component({
  selector: 'app-aluno',
  templateUrl: './aluno.component.html',
  styleUrls: ['./aluno.component.scss']
})
export class AlunoComponent extends FormPageComponent<AlunoModel>
  implements OnInit {
  public usuarios = [] as UsuarioModel[];
  public usuario = new UsuarioModel();

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected alunosService: AlunosService,
    protected usuariosService: UsuariosService,
    protected messagesService: MessagesService,
    protected modalsService: ModalsService
  ) {
    super(route, alunosService, router, messagesService);
    this.routeBase = 'alunos';
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.getUsuarios(params['id']);

      this.initModel(params['id']).then(() => {
        this.initToolbar();
      });
    });
  }

  public async getUsuarios(id?: string) {
    await this.usuariosService.get().then((data) => (this.usuarios = data));
    this.selectUsuario(id);
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
