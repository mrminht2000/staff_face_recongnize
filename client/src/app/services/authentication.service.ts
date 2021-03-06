import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { authToken } from '../common/constant';
import { AuthenticateParam } from '../models/auth/authenticate-param';
import { AuthResponseDto } from '../models/auth/dtos/get-authentication';
import { User } from '../models/user/user.model';
import { parseJwt } from '../common/jwt-heplers';
import { CookieService } from 'ngx-cookie';
import { NotificationService } from './notification.service';


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
    private readonly router: Router,
    private readonly cookieService: CookieService,
    private readonly notifitcation: NotificationService
  ) {
    this.authenApi = new URL(`/api/authentication`, environment.apiUrl).href;
    this.currentUser$.asObservable();
    var token = this.cookieService.get(authToken);

    if (!!token && token != '') {
      this.jwtToCurrentUser(token);
    }
  }

  public get currentUser(): User {
    return this.currentUser$.value;
  }

  public loginUser(body: AuthenticateParam) {
    return this.http.post<AuthResponseDto>(this.authenApi, body)
    .subscribe({
      next: (result) => {
        this.cookieService.put(authToken, result.token, { expires: new Date(result.expires) })
        this.jwtToCurrentUser(result.token);

        this.router.navigate(['']);
        this.notifitcation.showSuccess("Đăng nhập thành công");
      },
      error: () => {
        this.notifitcation.showError("Sai tài khoản hoặc mật khẩu")
      }
    });
  }

  public logout() {
    this.currentUser$.next({} as User);
    this.cookieService.remove(authToken);
    this.router.navigate(['auth/signin']);
  }

  jwtToCurrentUser(token: string) {
    var currentUser = parseJwt(token);
    this.currentUser$.next(currentUser);
  }
}
