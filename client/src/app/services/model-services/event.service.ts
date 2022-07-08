import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Event } from 'src/app/models/event/event.model'
import { GetEventResp } from 'src/app/models/event/dtos/get-event-resp';
import { CreateEventReq } from 'src/app/models/event/dtos/create-event-req';
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

  createCompantEvent(req: CreateEventReq) {
    return this.httpClient.post(this.eventApi + '/create', req);
  }

  createUserEvent(req: CreateEventReq) {
    return this.httpClient.post(this.eventApi + '/event', req, {
      params: {userid: this.authService.currentUser.id}
    });
  }

  createVacationByUser(req: CreateEventReq) {
    return this.httpClient.post(this.eventApi + '/vacation', req, {
      params: {userid: this.authService.currentUser.id}
    });
  }

  getEvent(id: number) {
    return this.httpClient.get<Event>(this.eventApi + '/event', {
      params: {id: id, userid: this.authService.currentUser.id}
    }).pipe(
      catchError(() => of({} as Event))
    );
  }

  getEventsByUser(userId: number) {
    return this.httpClient.get<GetEventResp>(this.eventApi + '/user', {
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

  updateEvent(event: Event) {
    return this.httpClient.put(this.eventApi + '/update', event)
  }

  acceptEvent(event: Event) {
    return this.httpClient.put(this.eventApi + '/confirmed', event)
  }

  deleteEvent(id: number) {
    return this.httpClient.delete(this.eventApi + '/delete', {
      params: {eventId: id}
    })
  }
}
