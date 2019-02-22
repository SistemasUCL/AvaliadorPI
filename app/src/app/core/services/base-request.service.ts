import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, EventEmitter, } from '@angular/core';

@Injectable()
export class BaseRequestService {
  constructor(protected http: HttpClient) { }

  protected getApiUrl(path: string): string {
    return `${environment.API_URL}/${path}`;
  }

  protected get(path: string): Promise<any> {
    const url = this.getApiUrl(path);

    return this.http.get(url).toPromise();
  }

  protected post(path: string, body: any): Promise<any> {
    const url = this.getApiUrl(path);

    if (!body.id) {
      return this.http.post(url, body).toPromise();
    } else {
      return this.http.put(url, body).toPromise();
    }
  }

  protected delete(path: string): Promise<any> {
    const url = this.getApiUrl(path);

    return this.http.delete(url).toPromise();
  }
}
