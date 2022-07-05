import { User } from "../user/user.model";

export interface Event {
    id: number;

    eventType: number;

    eventName: string;

    startTime: Date;

    endTime: Date;

    per: string;

    allDay: boolean;

    isConfirmed: boolean;

    user: User;
}