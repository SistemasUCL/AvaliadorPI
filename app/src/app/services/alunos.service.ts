import { Injectable } from '@angular/core';

import { RequestService } from '../core/services/request.service';
import { HttpClient } from '@angular/common/http';
import { AlunoModel } from '../shared/models/aluno.model';
import { CriterioModel } from '../shared/models/criterio.model';
import { AvaliadorModel } from '../shared/models/avaliador.model';
import { AvaliacaoModel } from '../shared/models/avaliacao.model';
import { AlunoAvaliacaoModel } from '../shared/models/aluno-avaliacao.model';

@Injectable()
export class AlunosService extends RequestService<AlunoModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'alunos';
  }

  public getAvaliacao(alunoId: string): Promise<AlunoAvaliacaoModel> {
    const url = `${this.routeBase}/${alunoId}/avaliacao`;

    return this.getByUrl(url).then((data) => data as AlunoAvaliacaoModel);
  }

  public avaliar(avaliacao: AvaliacaoModel): Promise<AvaliacaoModel> {
    const url = `${this.routeBase}/${avaliacao.alunoId}/avaliacao`;

    return this.post(url, avaliacao).then((res) => res as AvaliacaoModel);
  }
}
