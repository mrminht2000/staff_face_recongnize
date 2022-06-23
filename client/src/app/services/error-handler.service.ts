import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {
  
  constructor(private readonly router: Router,
              private readonly authService: AuthenticationService
             ) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        this.handleError(error);
        return throwError(() => new Error(error.message));
      })
    )
  }

  private handleError(error: HttpErrorResponse) : void {
    switch (error.status) {
      case 400: {
        this.handleBadRequest(error);
        break;
      }
      case 401: {
        this.handleUnauthorized(error);
        break;
      }
      case 404: {
        this.handleNotFound(error);
        break;
      }
      case 500: {
        this.handleInternetError(error);
        break;
      }
    }
  } 

  private handleInternetError(error: HttpErrorResponse) {
    alert(error.error.Message);
  }

  private handleUnauthorized(error: HttpErrorResponse) {
    this.authService.logout();
  }

  private handleNotFound(error: HttpErrorResponse) { }

  private handleBadRequest(error: HttpErrorResponse) { 
    Object.entries(error.error.errors).forEach( ([key, value]) => {
      alert(key + ': ' + value)
    })
  }
}