import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { ListPageComponent } from '../_base/list-page.component';
import { ProjetoModel } from '../../shared/models/projeto.model';
import { MessagesService } from '../../core/services/messages.service';
import { ProjetosService } from '../../services/projetos.service';
import { GridProjetoColumns } from '../../shared/grid-columns/grid-projeto.columns';

@Component({
  selector: 'app-projetos',
  templateUrl: './projetos.component.html',
  styleUrls: ['./projetos.component.scss']
})
export class ProjetosComponent extends ListPageComponent<ProjetoModel>
  implements OnInit {
  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected projetosService: ProjetosService
  ) {
    super(router, messagesService, projetosService);
    this.routeBase = 'projetos';
  }

  ngOnInit() {
    super.ngOnInit();
    this.initGrid(GridProjetoColumns);
  }
}
