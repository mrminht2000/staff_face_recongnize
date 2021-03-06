import { Component, OnDestroy, OnInit } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/angular';
import interactionPlugin from '@fullcalendar/interaction';
import dayGridPlugin from '@fullcalendar/daygrid';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'src/app/services/model-services/event.service';
import { toEventCalendarArray } from 'src/app/common/event-helpers';
import { EventCalendar } from 'src/app/models/event/event-calendar';
import { Event } from 'src/app/models/event/event.model';
import { DialogService } from 'src/app/services/dialog.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-staffs-calendar',
  templateUrl: './staffs-calendar.component.html',
  styleUrls: ['./staffs-calendar.component.scss'],
})
export class StaffsCalendarComponent implements OnInit, OnDestroy {
  calendarOptions: CalendarOptions = {};
  externalEvent = [];
  eventResult: Event[] = [];
  destroyed$ = new Subject<boolean>();
  userId = 0;
  afterGetEvent$ = new BehaviorSubject<EventCalendar[]>([]);
  onQueryEvent$ = new Subject();

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly eventService: EventService,
    private readonly authService: AuthenticationService,
    private readonly dialog: DialogService,
    private readonly router: Router
  ) {
    this.activatedRoute.params.pipe(
      takeUntil(this.destroyed$)
    ).subscribe(params => {
      this.userId = params['id'];
    })
  }

  ngOnInit(): void { 
    this.onQueryEvent$.subscribe((res) => {
      this.eventService.getEventsByUser(this.userId).subscribe((res) => {
        this.eventResult = res.events;
        this.afterGetEvent$.next(toEventCalendarArray(res.events));
      });
    });

    this.afterGetEvent$.pipe(
    ).subscribe(events =>{
      this.calendarOptions = {
        plugins: [dayGridPlugin, interactionPlugin],
        initialView: 'dayGridMonth',
        headerToolbar: {
          left: 'prev,next',
          center: 'title',
          right: 'dayGridDay,dayGridWeek,dayGridMonth',
        },
        themeSystem: 'bootstrap',
        weekends: true,
        events: events,
        editable: false,
        droppable: false,
        contentHeight: 'auto',
        eventClick: (arg) => {
          this.dialog.openEventDetail(this.eventResult.find((event) => event.id.toString() == arg.event.id) || {} as Event);
          this.reloadAfterChange();
        }
      };
    })

    this.onQueryEvent$.next(true);
  }

  ngOnDestroy(): void {
    this.destroyed$.next(true);
  }

  openVacationDialog() {
    this.dialog.openCreateVacation();
    this.reloadAfterChange();
  }

  openEventDialog() {
    this.dialog.openCreateEvent();
    this.reloadAfterChange();
  }

  isOwner(){
    return this.authService.currentUser.id == this.userId;
  }

  reloadAfterChange(){
    this.dialog.confirmed().subscribe(res => {
      if (res) {
        this.onQueryEvent$.next(true);
      }
    })
  }
}
