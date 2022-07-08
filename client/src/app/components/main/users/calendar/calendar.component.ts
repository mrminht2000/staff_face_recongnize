import { Component, OnInit } from '@angular/core';
import { Calendar, CalendarOptions } from '@fullcalendar/angular';
import interactionPlugin from '@fullcalendar/interaction';
import dayGridPlugin from '@fullcalendar/daygrid';
import { EventCalendar } from 'src/app/models/event/event-calendar';
import { BehaviorSubject, Subject } from 'rxjs';
import { EventService } from 'src/app/services/model-services/event.service';
import { toEventCalendarArray } from 'src/app/common/event-helpers';
import { Event } from 'src/app/models/event/event.model';
import { DialogService } from 'src/app/services/dialog.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss'],
})
export class CalendarComponent implements OnInit {
  calendarOptions: CalendarOptions = {};
  externalEvent = [];
  eventResult = [] as Event[];
  afterGetEvent$ = new BehaviorSubject<EventCalendar[]>([]);
  onQueryEvent$ = new Subject();

  constructor(
    private readonly eventService: EventService,
    private readonly dialog: DialogService
  ) {
    const name = Calendar.name;
  }

  ngOnInit(): void {
    this.onQueryEvent$.subscribe((res) => {
      this.eventService.getCompanyEvents().subscribe((res) => {
        this.eventResult = res.events;
        this.afterGetEvent$.next(toEventCalendarArray(res.events));
      });
    });

    this.afterGetEvent$.pipe().subscribe((events) => {
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
          this.dialog.openEventDetail(
            this.eventResult.find(
              (event) => event.id.toString() == arg.event.id
            ) || ({} as Event)
          );
          this.reloadAfterChange();
        },
      };
    });

    this.onQueryEvent$.next(true);
  }

  reloadAfterChange(){
    this.dialog.confirmed().subscribe(res => {
      if (res) {
        this.onQueryEvent$.next(true);
      }
    })
  }

  openCreateVacation() {
    this.dialog.openCreateVacation(true);
    this.reloadAfterChange();
  }

  openCreateEvent() {
    this.dialog.openCreateEvent(true);
    this.reloadAfterChange();
  }
}
