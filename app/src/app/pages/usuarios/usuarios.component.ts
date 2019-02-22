import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { ListPageComponent } from '../_base/list-page.component';
import { UsuarioModel } from '../../shared/models/usuario.model';
import { MessagesService } from '../../core/services/messages.service';
import { UsuariosService } from '../../services/usuarios.service';
import { GridUsuarioColumns } from '../../shared/grid-columns/grid-usuario.columns';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss']
})
export class UsuariosComponent extends ListPageComponent<UsuarioModel>
  implements OnInit {
  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected usuariosService: UsuariosService
  ) {
    super(router, messagesService, usuariosService);
    this.routeBase = 'usuarios';
  }

  ngOnInit() {
    super.ngOnInit();
    this.initGrid(GridUsuarioColumns);
  }
}
