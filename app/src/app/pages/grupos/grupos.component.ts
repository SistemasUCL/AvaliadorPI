import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { ListPageComponent } from '../_base/list-page.component';
import { MessagesService } from '../../core/services/messages.service';
import { GruposService } from '../../services/grupos.service';
import { GridGrupoColumns } from '../../shared/grid-columns/grid-grupo.columns';
import { GrupoModel } from '../../shared/models/grupo.model';

@Component({
  selector: 'app-grupos',
  templateUrl: './grupos.component.html',
  styleUrls: ['./grupos.component.scss']
})
export class GruposComponent extends ListPageComponent<GrupoModel>
  implements OnInit {
  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected gruposService: GruposService
  ) {
    super(router, messagesService, gruposService);
    this.routeBase = 'grupos';
  }

  ngOnInit() {
    super.ngOnInit();
    this.initGrid(GridGrupoColumns);
  }
}
