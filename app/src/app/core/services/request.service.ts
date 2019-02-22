import { Http, Headers, RequestOptions } from '@angular/http';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseRequestService } from './base-request.service';
import { BaseModel } from '../models/base.model';

@Injectable()
export class RequestService<T extends BaseModel> extends BaseRequestService {
  protected routeBase: string;

  constructor(protected http: HttpClient) {
    super(http);
  }

  public get(): Promise<T[]> {
    return super.get(this.routeBase).then((data) => data.data as T[]);
  }

  public getById(id: number): Promise<T> {
    const url = `${this.routeBase}/${id}`;

    return super.get(url).then((data) => data as T);
  }

  public getByUrl(url: string): Promise<any> {
    return super.get(url).then((data) => data);
  }

  public save(model: T): Promise<T> {
    const url = `${this.routeBase}/${model.id ? model.id : ''}`;

    return super.post(url, model).then((res) => res as T);
  }

  public delete(id): Promise<any> {
    const url = `${this.routeBase}/${id}`;

    return super.delete(url).then((data) => data);
  }
}
