import { Injectable } from '@angular/core';

import { RequestService } from '../core/services/request.service';
import { HttpClient } from '@angular/common/http';
import { ProfessorModel } from '../shared/models/professor.model';

@Injectable()
export class ProfessoresService extends RequestService<ProfessorModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'professores';
  }
}
