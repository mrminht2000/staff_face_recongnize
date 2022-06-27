import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  Router,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import { Observable } from 'rxjs';

import { AuthenticationService } from '../authentication.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService implements CanActivate, CanActivateChild {
  constructor(
    private readonly authService: AuthenticationService,
    private readonly router: Router
  ) {}

  canActivateChild(childRoute: ActivatedRouteSnapshot) {
    const expectedRole = childRoute.data['expectedRole'] || 0;

    if (!this.authService.currentUser.id) {
      this.router.navigate(['/auth/signin']);
      return false;
    }

    if(this.authService.currentUser.role < expectedRole) {
      this.router.navigate(['']);
      return false;
    }
    
    return true;
  }

  canActivate(route: ActivatedRouteSnapshot) {
    const expectedRole = route.data['expectedRole'] || 0;

    if (!this.authService.currentUser.id) {
      this.router.navigate(['/auth/signin']);
      return false;
    }
    
    if(this.authService.currentUser.role < expectedRole) {
      this.router.navigate(['']);
      return false;
    }

    return true;
  }
}
