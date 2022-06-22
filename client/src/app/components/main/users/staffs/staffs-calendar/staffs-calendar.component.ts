import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Calendar } from '@fullcalendar/core';
import { CalendarOptions, FullCalendarComponent } from '@fullcalendar/angular';
import interactionPlugin, { Draggable } from '@fullcalendar/interaction';
import dayGridPlugin from '@fullcalendar/daygrid';

@Component({
  selector: 'app-staffs-calendar',
  templateUrl: './staffs-calendar.component.html',
  styleUrls: ['./staffs-calendar.component.scss'],
})
export class StaffsCalendarComponent implements OnInit, AfterViewInit {
  calendarOptions: CalendarOptions = {};
  externalEvent = [];
  
  event = [
    {
      title: 'event 1',
      color: '#FF5733',
      date: '2022-06-31', 
    },
    {
      title: 'event 2',
      date: '2022-06-28',
    },
    {
      title: 'event 3',
      date: '2022-06-01',
    },
  ];

  @ViewChild('calendar') calendarComponent!: FullCalendarComponent;

  constructor() {
    const name = Calendar.name;
  }

  ngOnInit(): void {
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
      events: this.event,
      editable: false,
      droppable: false,
      contentHeight: 'auto',
    };

    this.calendarOptions.eventContent = (arg) => {
      if(arg.event.title == '*') {
        
      }
      let content = document.createElement('div');
      content.className = 'fc-event-content';
      content.innerHTML = arg.event.title;
      
      if (arg.event.title[0] != '*') {
        return { domNodes: [content] };
      }

      let icon = document.createElement('i');
      icon.className = 'fc-event-delete fa fa-times';

      icon.addEventListener('click', (e: Event) => {
        e.stopPropagation(); 
        arg.event.remove();
      });

      let arrayOfDomNodes = [content, icon];
      return { domNodes: arrayOfDomNodes };
    };

    setTimeout(() => {
      this.calendarComponent.getApi().render();
    }, 100);
  }

  ngAfterViewInit() {
    let draggableEl = document.getElementById('external-events')!;

    new Draggable(draggableEl, {
      itemSelector: '.external-event',
      eventData: (eventEl) => {
        return {
          title: eventEl.innerText.trim(),
          color: eventEl.dataset['color'],
          className: "external-event-dropped"
        };
      },
    });
  }
}
