import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { DisciplinaModel } from '../../shared/models/disciplina.model';
import { FormPageComponent } from '../_base/form-page.component';
import { DisciplinasService } from '../../services/disciplinas.service';
import { MessagesService } from '../../core/services/messages.service';

@Component({
  selector: 'app-disciplina',
  templateUrl: './disciplina.component.html',
  styleUrls: ['./disciplina.component.scss']
})
export class DisciplinaComponent extends FormPageComponent<DisciplinaModel>
  implements OnInit {
  constructor(
    protected route: ActivatedRoute,
    protected disciplinasService: DisciplinasService,
    protected router: Router,
    protected messagesService: MessagesService
  ) {
    super(route, disciplinasService, router, messagesService);
    this.routeBase = 'disciplinas';
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
