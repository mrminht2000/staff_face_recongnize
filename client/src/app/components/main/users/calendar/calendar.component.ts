import { Component, OnInit } from '@angular/core';
import { Calendar, CalendarOptions } from '@fullcalendar/angular';
import interactionPlugin from '@fullcalendar/interaction';
import dayGridPlugin from '@fullcalendar/daygrid';
import { EventCalendar } from 'src/app/models/event/event-calendar';
import { BehaviorSubject } from 'rxjs';
import { EventService } from 'src/app/services/model-services/event.service';
import { toEventCalendarArray } from 'src/app/common/event-helpers';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {
  calendarOptions: CalendarOptions = {};
  externalEvent = [];
  events = [] as EventCalendar[];
  afterGetEvent$ = new BehaviorSubject<EventCalendar[]>([]);

  constructor(
    private readonly eventService: EventService
  ) {
    const name = Calendar.name
  }

  ngOnInit(): void {
    this.eventService.getCompanyEvents().subscribe(res => {
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
}
