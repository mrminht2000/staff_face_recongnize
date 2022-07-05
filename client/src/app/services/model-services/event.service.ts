import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Event } from 'src/app/models/event/event.model'
import { GetEventResp } from 'src/app/models/event/dtos/get-event-resp';
import { CreateEventReq } from 'src/app/models/event/dtos/create-event-req';
import { User } from 'src/app/models/user/user.model';
import { GetUserResp } from 'src/app/models/user/dtos/get-user-resp';
import { AuthenticationService } from '../authentication.service';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private readonly eventApi: string;

  constructor(
    private readonly httpClient: HttpClient,
    private readonly authService: AuthenticationService
  ) { 
    this.eventApi =  new URL('/api/event', environment.apiUrl).href;
  }

  createVacationByUser(req: CreateEventReq) {
    return this.httpClient.post<Event>(this.eventApi + '/vacation', req, {
      params: {userid: this.authService.currentUser.id}
    }).pipe(
      catchError(() => of({} as Event))
    );
  }

  getEventsByUser(userId: number) {
    return this.httpClient.get<GetEventResp>(this.eventApi + '/info', {
      params: {userId: userId}
    }).pipe(
      catchError(() => of({} as GetEventResp))
    );
  }

  getCompanyEvents() {
    return this.httpClient.get<GetEventResp>(this.eventApi).pipe(
      catchError(() => of({} as GetEventResp))
    );
  }

  getEventById(id: number) {
    return this.httpClient.get<Event>(this.eventApi + '/info', {
      params: {eventId: id}
    }).pipe(
      catchError(() => of({} as Event))
    );
  }

  getUnconfirmEvents() {
    return this.httpClient.get<GetUserResp>(this.eventApi + '/unconfirmed').pipe(
      catchError(() => of({} as GetUserResp))
    );
  }

  getUnconfirmEventsByUserId(userId: number) {
    return this.httpClient.get<GetEventResp>(this.eventApi + '/unconfirmed/user', {
      params: {userId: userId}
    }).pipe(
      catchError(() => of({} as GetEventResp))
    );
  }

  acceptEvent(event: Event) {
    return this.httpClient.put<Event>(this.eventApi + '/confirmed', event)
  }

  declineEvent(id: number) {
    return this.httpClient.delete<Event>(this.eventApi + '/decline', {
      params: {eventId: id}
    })
  }
}
