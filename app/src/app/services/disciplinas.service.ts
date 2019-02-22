import { Injectable } from '@angular/core';

import { RequestService } from '../core/services/request.service';
import { HttpClient } from '@angular/common/http';
import { DisciplinaModel } from '../shared/models/disciplina.model';

@Injectable()
export class DisciplinasService extends RequestService<DisciplinaModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'disciplinas';
  }
}
