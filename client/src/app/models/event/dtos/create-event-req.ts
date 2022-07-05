export interface CreateEventReq {
    eventType: number;

    eventName: string;

    startTime: Date;

    endTime: Date;

    per: string;

    allDay: boolean;

    userId: number;
}