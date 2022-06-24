import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from '../authentication.service';
import { CookieService } from 'ngx-cookie';
import { authToken } from 'src/app/common/constant';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationHeaderService {

  constructor(
    private readonly authService: AuthenticationService,
    private readonly cookieService: CookieService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this.cookieService.get(authToken);
        const isLoggedIn = !!this.authService.currentUser.id;
        if (isLoggedIn && !!token) {
            request = request.clone({
                setHeaders: { 
                  Authorization: `Bearer ${token}` 
                }
            });
        }

        return next.handle(request);
    }
}
