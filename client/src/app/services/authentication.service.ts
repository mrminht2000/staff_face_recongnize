import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { authToken } from '../common/constant';
import { AuthenticateParam } from '../models/auth/authenticate-param';
import { AuthResponseDto } from '../models/auth/dtos/get-authentication';
import { User } from '../models/auth/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private authenApi: string;
  private currentUser$: BehaviorSubject<User> = new BehaviorSubject<User>(
    {} as User
  );

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {
    this.authenApi = new URL(`/api/authentication`, environment.apiUrl).href;
    this.currentUser$.asObservable();
    var token = localStorage.getItem(authToken) || '';

    if (!!token && token != '') {
      this.jwtToCurrentUser(token);
    }
  }

  public get currentUser(): User {
    return this.currentUser$.value;
  }

  public loginUser(body: AuthenticateParam) {
    return this.http.post<AuthResponseDto>(this.authenApi, body).subscribe({
      next: (result) => {
        localStorage.setItem(authToken, result.token);
        this.jwtToCurrentUser(result.token);

        this.router.navigate(['']);
        console.log("Dang nhap thanh cong")
      },
      error: () => {
        console.log("Dang nhap that bai")
      },
    });
  }

  public logout() {
    this.currentUser$.next({} as User);
    localStorage.removeItem(authToken);
    this.router.navigate(['auth/signin']);
  }

  jwtToCurrentUser(token: string) {
    var tokenPayload = this.parseJwt(token);

    this.currentUser$.next({
      id: tokenPayload.nameid,
      userName: tokenPayload.email,
      fullName: tokenPayload.given_name,
    });
  }

  parseJwt(token: string) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join('')
    );

    return JSON.parse(jsonPayload);
  }
}
