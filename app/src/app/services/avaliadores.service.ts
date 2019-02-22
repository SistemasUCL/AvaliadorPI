import { Injectable } from '@angular/core';

import { RequestService } from '../core/services/request.service';
import { HttpClient } from '@angular/common/http';
import { AvaliadorModel } from '../shared/models/avaliador.model';

@Injectable()
export class AvaliadoresService extends RequestService<AvaliadorModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'avaliadores';
  }
}
