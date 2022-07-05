import { EventCalendar } from "../models/event/event-calendar";
import { Event } from "../models/event/event.model";
import { EventType } from "./constant";

export const toEventCalendar = (event: Event): EventCalendar => {
    var color
    switch(event.eventType) {
        case EventType.Default: {
            break;
        }

        case EventType.Register: {
            color = '#28a745';
            break;
        }

        case EventType.Vacation: {
            color = '#ffc107';
            break;
        }

        case EventType.Absent: {
            color = '#dc3545';
            break;
        }
    }

    if (!event.isConfirmed) {
        event.eventName = '(*)' + event.eventName;
    }

    return {
        id: event.id.toString(),
        title: event.eventName,
        color: color,
        start: event.startTime,
        end: event.endTime,
        allDay: event.allDay
    } as EventCalendar
}

export const toEventCalendarArray = (events: Event[]): EventCalendar[] => {
    return events?.map(event => toEventCalendar(event));
}