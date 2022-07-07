import { UserData } from "../models/user/user-data";
import { User } from "../models/user/user.model";

export const toUserData = (user: User): UserData => {
    return {
        id: user.id,
        userName: user.userName,
        fullName: user.fullName,
        role: user.role,
        phoneNumber: user.phoneNumber,
        email: user.email,
        isConfirmed: user.isConfirmed,
        departmentId: user.department.id,
        jobId: user.job.id
    }
}