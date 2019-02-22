import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { RequestService } from '../core/services/request.service';
import { UsuarioModel } from '../shared/models/usuario.model';

@Injectable()
export class UsuariosService extends RequestService<UsuarioModel> {
  constructor(protected http: HttpClient) {
    super(http);
    this.routeBase = 'usuarios';
  }
}
