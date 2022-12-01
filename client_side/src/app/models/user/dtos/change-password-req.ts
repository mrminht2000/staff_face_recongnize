export interface ChangePasswordReq {
    userId: number;

    oldPassword: string;

    newPassword: string;

    rePassword: string;
}