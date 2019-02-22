import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ListPageComponent } from '../_base/list-page.component';
import { ProfessorModel } from '../../shared/models/professor.model';
import { MessagesService } from '../../core/services/messages.service';
import { GridProfessorColumns } from '../../shared/grid-columns/grid-professor.columns';
import { ProfessoresService } from '../../services/professores.service';

@Component({
  selector: 'app-professores',
  templateUrl: './professores.component.html',
  styleUrls: ['./professores.component.scss']
})
export class ProfessoresComponent extends ListPageComponent<ProfessorModel>
  implements OnInit {
  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected professoresService: ProfessoresService
  ) {
    super(router, messagesService, professoresService);
    this.routeBase = 'professores';
  }

  ngOnInit() {
    super.ngOnInit();
    this.initGrid(GridProfessorColumns);
  }
}
