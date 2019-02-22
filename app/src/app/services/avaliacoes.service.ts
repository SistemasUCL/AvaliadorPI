import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { RequestService } from '../core/services/request.service';
import { AvaliacaoModel } from '../shared/models/avaliacao.model';

@Injectable()
export class AvaliacoesService extends RequestService<AvaliacaoModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'avaliacoes';
  }
}
