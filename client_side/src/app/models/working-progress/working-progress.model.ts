import { User } from "../user/user.model";

export interface WorkingProgress {
    id: number;

    workingDayInMonth: number;

    lateTimeByHours: number;

    lastUpdate: Date;

    user: User;
}
