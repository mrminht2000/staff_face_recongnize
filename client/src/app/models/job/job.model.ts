import { User } from "../user/user.model";

export interface Job {
    id: number;

    name: string;

    description: string;

    salary: number;

    salaryPer: string;

    users: User[];
}