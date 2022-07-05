import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  private readonly loading$ : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  
  isLoading() : Observable<boolean> {
    return this.loading$.asObservable();
  }

  showLoading() {
    this.loading$.next(true);
  }

  hideLoading() {
    this.loading$.next(false);
  }
}
