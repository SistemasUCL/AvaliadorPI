import { Component, OnInit } from '@angular/core';
import { ListPageComponent } from '../_base/list-page.component';
import { AlunoModel } from '../../shared/models/aluno.model';
import { Router } from '@angular/router';
import { MessagesService } from '../../core/services/messages.service';
import { GridAlunoColumns } from '../../shared/grid-columns/grid-aluno.columns';
import { AlunosService } from '../../services/alunos.service';

@Component({
  selector: 'app-alunos',
  templateUrl: './alunos.component.html',
  styleUrls: ['./alunos.component.scss']
})
export class AlunosComponent extends ListPageComponent<AlunoModel>
  implements OnInit {
  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected alunosService: AlunosService
  ) {
    super(router, messagesService, alunosService);
    this.routeBase = 'alunos';
  }

  ngOnInit() {
    super.ngOnInit();
    this.initGrid(GridAlunoColumns);
  }
}
