import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ListPageComponent } from '../_base/list-page.component';
import { MessagesService } from '../../core/services/messages.service';
import { ProfessoresService } from '../../services/professores.service';
import { GridAdministradorColumns } from '../../shared/grid-columns/grid-administrador.columns';
import { AdministradoresService } from '../../services/administradores.service';
import { AdministradorModel } from '../../shared/models/administrador.model';

@Component({
  selector: 'app-administradores',
  templateUrl: './administradores.component.html',
  styleUrls: ['./administradores.component.scss']
})
export class AdministradoresComponent
  extends ListPageComponent<AdministradorModel>
  implements OnInit {
  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected administradoresService: AdministradoresService
  ) {
    super(router, messagesService, administradoresService);
    this.routeBase = 'administradores';
  }

  ngOnInit() {
    super.ngOnInit();
    this.initGrid(GridAdministradorColumns);
  }
}
