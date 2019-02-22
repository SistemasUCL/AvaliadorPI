import { Injectable } from '@angular/core';

import { RequestService } from '../core/services/request.service';
import { HttpClient } from '@angular/common/http';
import { AdministradorModel } from '../shared/models/administrador.model';

@Injectable()
export class AdministradoresService extends RequestService<AdministradorModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'administradores';
  }
}
