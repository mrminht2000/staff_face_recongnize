import { Department } from "../department/department.model";
import { Job } from "../job/job.model";
import { Event } from "../event/event.model"

export interface User {
    id: number;

    userName: string;

    fullName: string;

    role: number;

    phoneNumber: string;

    email: string;

    startDay: Date;

    isConfirmed: boolean;

    department: Department;

    job: Job;

    events: Event[];
}