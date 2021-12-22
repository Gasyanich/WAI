import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from "../../environments/environment";
import {AuthService} from "../services/auth.service";

@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const reqWithApiUrlClone = request.clone(
      {url: environment.apiUrl + request.url}
    );

    if (this.authService.isAuthenticated()) {
      const token = localStorage.getItem('jwt');

      const authReq = reqWithApiUrlClone.clone({
        headers: reqWithApiUrlClone.headers.set('Authorization', `Bearer ${token}`),
      });

      return next.handle(authReq);
    }

    return next.handle(reqWithApiUrlClone);
  }
}
