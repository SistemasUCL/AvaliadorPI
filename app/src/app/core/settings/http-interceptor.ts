import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../services/auth.service';

@Injectable()
export class BaseHttpInterceptor implements HttpInterceptor {

  constructor(private auth: AuthService) {
  }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let headersList = req.headers;
    headersList = headersList.set('Authorization', this.auth.getAuthorizationHeaderValue());
    headersList = headersList.set('Content-Type', 'application/json');
    headersList = headersList.set('Access-Control-Allow-Origin', '*');
    headersList = headersList
      .set('Access-Control-Allow-Headers', 'Cache-Control, Pragma, Origin, Authorization, Content-Type, X-Requested-With');
    headersList = headersList.set('Access-Control-Allow-Methods', 'GET, PUT, POST, DELETE');

    const authReq = req.clone({ headers: headersList });

    return next
      .handle(authReq)
      .catch((error, caught) => {
        return Observable.throw(error);
      }) as any;
  }
}
