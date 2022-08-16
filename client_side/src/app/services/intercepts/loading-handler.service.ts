import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { LoadingService } from '../loading.service';

@Injectable({
  providedIn: 'root',
})
export class LoadingHandlerService implements HttpInterceptor {
  constructor(
    private readonly loadingService: LoadingService
  )
  { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loadingService.showLoading();
    return next.handle(req).pipe(finalize(() => this.loadingService.hideLoading()));
  }
}
