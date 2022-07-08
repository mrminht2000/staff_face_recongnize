import { EventCalendar } from "../models/event/event-calendar";
import { Event } from "../models/event/event.model";
import { EventType } from "./constant";

export const toEventCalendar = (event: Event): EventCalendar => {
    var color
    switch(event.eventType) {
        case EventType.Default: {
            color = EventColor.default;
            break;
        }

        case EventType.Register: {
            color = EventColor.register;
            break;
        }

        case EventType.Vacation: {
            color = EventColor.vacation;
            break;
        }

        case EventType.Absent: {
            color = EventColor.absent;
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

export const EventColor = { 
    default: '#007bff',
    register: '#28a745',
    vacation: '#ffc107',
    absent: '#dc3545'
} 