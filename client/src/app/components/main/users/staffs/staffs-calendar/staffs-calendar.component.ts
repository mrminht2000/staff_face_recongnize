import { Component, OnDestroy, OnInit } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/angular';
import interactionPlugin from '@fullcalendar/interaction';
import dayGridPlugin from '@fullcalendar/daygrid';
import { BehaviorSubject, Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { EventService } from 'src/app/services/model-services/event.service';
import { toEventCalendarArray } from 'src/app/common/event-helpers';
import { EventCalendar } from 'src/app/models/event/event-calendar';
import { MatDialog } from '@angular/material/dialog';
import { CreateVacationComponent } from '../../calendar/dialogs/create-vacation/create-vacation.component';

@Component({
  selector: 'app-staffs-calendar',
  templateUrl: './staffs-calendar.component.html',
  styleUrls: ['./staffs-calendar.component.scss'],
})
export class StaffsCalendarComponent implements OnInit, OnDestroy {
  calendarOptions: CalendarOptions = {};
  externalEvent = [];
  events = [] as EventCalendar[];
  routeSub = new Subscription();
  userId = 0;
  afterGetEvent$ = new BehaviorSubject<EventCalendar[]>([]);

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly eventService: EventService,
    private readonly dialog: MatDialog
  ) {
    this.routeSub = this.activatedRoute.params.subscribe(params => {
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
  }

  ngOnDestroy(): void {
    this.routeSub.unsubscribe();
  }

  openVacationDialog() {
    this.dialog.open(CreateVacationComponent, {
      width: '700px'
    })
  }
}
