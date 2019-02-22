import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { FormPageComponent } from '../_base/form-page.component';
import { UsuarioModel } from '../../shared/models/usuario.model';
import { UsuariosService } from '../../services/usuarios.service';
import { MessagesService } from '../../core/services/messages.service';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.scss']
})
export class UsuarioComponent extends FormPageComponent<UsuarioModel>
  implements OnInit {
  constructor(
    protected route: ActivatedRoute,
    protected usuariosService: UsuariosService,
    protected router: Router,
    protected messagesService: MessagesService
  ) {
    super(route, usuariosService, router, messagesService);
    this.routeBase = 'usuarios';
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
