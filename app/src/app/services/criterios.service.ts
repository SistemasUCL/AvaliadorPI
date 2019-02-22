import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { RequestService } from '../core/services/request.service';
import { CriterioModel } from '../shared/models/criterio.model';

@Injectable()
export class CriteriosService extends RequestService<CriterioModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'criterios';
  }
}
