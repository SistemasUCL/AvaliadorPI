import { Component, OnInit, ViewChild } from '@angular/core';
import { ListPageComponent } from '../_base/list-page.component';
import { Router } from '@angular/router';

import { DisciplinaModel } from '../../shared/models/disciplina.model';
import { MessagesService } from '../../core/services/messages.service';
import { DisciplinasService } from '../../services/disciplinas.service';
import { GridDisciplinaColumns } from '../../shared/grid-columns/grid-disicplina.columns';

@Component({
  selector: 'app-disciplinas',
  templateUrl: './disciplinas.component.html',
  styleUrls: ['./disciplinas.component.scss']
})
export class DisciplinasComponent extends ListPageComponent<DisciplinaModel>
  implements OnInit {
  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected disciplinasService: DisciplinasService
  ) {
    super(router, messagesService, disciplinasService);
    this.routeBase = 'disciplinas';
  }

  ngOnInit() {
    super.ngOnInit();
    this.initGrid(GridDisciplinaColumns);
  }
}
