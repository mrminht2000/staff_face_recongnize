import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../authentication.service';

@Injectable({
  providedIn: 'root'
})
export class LoginGuardService implements CanActivate, CanActivateChild {

  constructor(
    private readonly authService: AuthenticationService,
    private readonly router: Router
  ) { }
  
  canActivateChild() {
    if (this.authService.currentUser.id) {
      this.router.navigate(['']);
      return false;
    }

    return true;
  }

  canActivate() {
    if (this.authService.currentUser.id) {
      this.router.navigate(['']);
      return false;
    }

    return true;
  }
}
