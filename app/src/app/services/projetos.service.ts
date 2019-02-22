import { Injectable } from '@angular/core';

import { RequestService } from '../core/services/request.service';
import { HttpClient } from '@angular/common/http';
import { ProjetoModel } from '../shared/models/projeto.model';
import { CriterioModel } from '../shared/models/criterio.model';
import { EstadoProjetoEnum } from '../shared/enums/estado-projeto.enum';

@Injectable()
export class ProjetosService extends RequestService<ProjetoModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'projetos';
  }

  public addAvaliador(id: string, avaliadorId: string): Promise<any> {
    const url = `${this.routeBase}/${id}/avaliadores`;

    return super.post(url, { avaliadorId: avaliadorId });
  }

  removeAvaliador(id: string, avaliadorId: string): Promise<any> {
    const url = this.getApiUrl(
      `${this.routeBase}/${id}/avaliadores/${avaliadorId}`
    );

    return this.http.delete(url).toPromise();
  }

  public addCriterio(id: string, criterioModel: CriterioModel): Promise<any> {
    let url = `${this.routeBase}/${id}/criterios`;

    if (criterioModel.id) {
      url = `${url}/${criterioModel.id}`;
    }

    return super.post(url, criterioModel);
  }

  public removeCriterio(id: string, criterioId: string): Promise<any> {
    const url = this.getApiUrl(
      `${this.routeBase}/${id}/criterios/${criterioId}`
    );

    return this.http.delete(url).toPromise();
  }

  public changeEstado(id: string, estado: EstadoProjetoEnum) {
    const url = `${this.routeBase}/${id}/estado-projeto`;
    return super.post(url, { estado: estado });
  }
}
