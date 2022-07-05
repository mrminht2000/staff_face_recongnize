import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, of } from 'rxjs';
import { GetUserResp } from 'src/app/models/user/dtos/get-user-resp';
import { User } from 'src/app/models/user/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly userApi: string;

  constructor(
    private readonly httpClient: HttpClient
  ) { 
    this.userApi =  new URL('/api/user', environment.apiUrl).href;
  }

  getUsers() {
    return this.httpClient.get<GetUserResp>(this.userApi).pipe(
      catchError(() => of({} as GetUserResp))
    );
  }

  getUsersEvents() {
    return this.httpClient.get<GetUserResp>(this.userApi + '/events').pipe(
      catchError(() => of({} as GetUserResp))
    );
  }

  getUserById(id: number) {
    return this.httpClient.get<User>(this.userApi + '/info', {
      params: {id: id}
    }).pipe(
      catchError(() => of({} as User))
    );
  }
}
