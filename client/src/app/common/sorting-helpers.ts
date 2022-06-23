import { Sort } from "@angular/material/sort";

import {SortedEvent} from '../models/sorted-event';

export const toSortedEvent = (event: Sort) => {
    return {
        sortField: event.active,
        sortDirection: event.direction === 'desc' ? 'desc' : 'asc'
    }as SortedEvent;
}