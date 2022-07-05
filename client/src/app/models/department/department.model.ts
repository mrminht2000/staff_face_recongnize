import { User } from "../user/user.model";

export interface Department {
    id: number;

    name: string;

    users: User[];
}