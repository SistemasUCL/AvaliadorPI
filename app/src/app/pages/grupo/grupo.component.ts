import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { FormPageComponent } from '../_base/form-page.component';

import { GruposService } from '../../services/grupos.service';
import { MessagesService } from '../../core/services/messages.service';
import { ProjetosService } from '../../services/projetos.service';

import { GrupoModel } from '../../shared/models/grupo.model';
import { ProjetoModel } from '../../shared/models/projeto.model';

@Component({
  selector: 'app-grupo',
  templateUrl: './grupo.component.html',
  styleUrls: ['./grupo.component.scss']
})
export class GrupoComponent extends FormPageComponent<GrupoModel>
  implements OnInit {
  public navOption = 'alunos';
  public projetos = [] as ProjetoModel[];
  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected gruposService: GruposService,
    protected projetosService: ProjetosService,
    protected messagesService: MessagesService
  ) {
    super(route, gruposService, router, messagesService);
    this.routeBase = 'grupos';
  }

  ngOnInit() {
    super.ngOnInit();
    this.getProjeto();
  }

  public getProjeto() {
    this.projetosService.get().then((data) => (this.projetos = data));
  }
}
