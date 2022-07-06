import { Component, OnDestroy, OnInit } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/angular';
import interactionPlugin from '@fullcalendar/interaction';
import dayGridPlugin from '@fullcalendar/daygrid';
import { BehaviorSubject, Subject, Subscription, takeUntil } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'src/app/services/model-services/event.service';
import { toEventCalendarArray } from 'src/app/common/event-helpers';
import { EventCalendar } from 'src/app/models/event/event-calendar';
import { MatDialog } from '@angular/material/dialog';
import { CreateVacationComponent } from '../../../../shared/dialogs/create-vacation/create-vacation.component'
import { CreateEventComponent } from '../../../../shared/dialogs/create-event/create-event.component';
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
  events = [] as EventCalendar[];
  destroyed$ = new Subject<boolean>();
  userId = 0;
  afterGetEvent$ = new BehaviorSubject<EventCalendar[]>([]);

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
    this.eventService.getEventsByUser(this.userId).subscribe(res => {
      this.afterGetEvent$.next(toEventCalendarArray(res.events));
    })

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
          console.log(arg.event.id);
        }
      };
    })

    this.dialog.confirmed().pipe(
      takeUntil(this.destroyed$)
    ).subscribe((confirmed) => {
      if (confirmed) {
        this.router
          .navigateByUrl('/', { skipLocationChange: true })
          .then(() =>
            this.router.navigate([
              '/staffs/calendar',
              this.authService.currentUser.id,
            ])
          );
      }
    })
  }

  ngOnDestroy(): void {
    this.destroyed$.next(true);
  }

  openVacationDialog() {
    this.dialog.openCreateVacation();
  }

  openEventDialog() {
    this.dialog.openCreateVacation();
  }

  isOwner(){
    return this.authService.currentUser.id == this.userId;
  }
}
