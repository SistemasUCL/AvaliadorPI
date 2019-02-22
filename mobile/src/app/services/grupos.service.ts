import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BaseRequestService } from '../core/services/base-request.service';
import { AvaliacaoModel } from '../shared/models/avaliacao.model';
import { AvaliacaoRetornoModel } from '../shared/models/avaliacao-retorno.model';

@Injectable()
export class GruposService extends BaseRequestService {
  routeBase = 'grupos';

  constructor(protected http: HttpClient) {
    super(http);
  }

  getAvaliacao(id: string): Promise<AvaliacaoModel> {
    const url = `${this.routeBase}/${id}/avaliacao`;

    return super.get(url);
  }

  addAvaliacao(id: string, avaliacao: AvaliacaoRetornoModel): Promise<any> {
    const url = this.getApiUrl(`${this.routeBase}/${id}/avaliacao`);

    return this.http.post(url, avaliacao).toPromise();
  }
}
