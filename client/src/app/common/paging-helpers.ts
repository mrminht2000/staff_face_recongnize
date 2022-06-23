import { PageEvent } from '@angular/material/paginator';

import { PageChangedEvent } from '../models/page-changed-event';

export const toPageChangedEvent = (pageEvent: PageEvent): PageChangedEvent => {
    return {
        skip: pageEvent.pageSize * pageEvent.pageIndex,
        take: pageEvent.pageSize,
        total: pageEvent.length
    } as PageChangedEvent;
}