import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { RequestService } from '../core/services/request.service';
import { GrupoModel } from '../shared/models/grupo.model';
import { IPromise } from 'q';
import { AlunoGrupoModel } from '../shared/models/aluno-grupo.model';

@Injectable()
export class GruposService extends RequestService<GrupoModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'grupos';
  }

  addAluno(id: string, alunoId: string): Promise<any> {
    const url = `${this.routeBase}/${id}/alunos`;

    return super.post(url, { alunoId: alunoId });
  }

  removeAluno(id: string, alunoId: string): Promise<any> {
    const url = this.getApiUrl(`${this.routeBase}/${id}/alunos/${alunoId}`);

    return this.http.delete(url).toPromise();
  }
}
