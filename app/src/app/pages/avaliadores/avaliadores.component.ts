import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ListPageComponent } from '../_base/list-page.component';
import { AvaliadorModel } from '../../shared/models/avaliador.model';
import { MessagesService } from '../../core/services/messages.service';
import { AvaliadoresService } from '../../services/avaliadores.service';
import { GridAvaliadorColumns } from '../../shared/grid-columns/grid-avaliador.columns';

@Component({
  selector: 'app-avaliadores',
  templateUrl: './avaliadores.component.html',
  styleUrls: ['./avaliadores.component.scss']
})
export class AvaliadoresComponent extends ListPageComponent<AvaliadorModel>
  implements OnInit {
  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected avaliadoresService: AvaliadoresService
  ) {
    super(router, messagesService, avaliadoresService);
    this.routeBase = 'avaliadores';
  }

  ngOnInit() {
    super.ngOnInit();
    this.initGrid(GridAvaliadorColumns);
  }
}
