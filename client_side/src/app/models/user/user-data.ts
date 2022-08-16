export interface UserData {
    id: number,
    
    userName: string;

    password?: string;

    fullName: string;

    role: number;

    phoneNumber: string;

    email: string;

    isConfirmed: boolean;

    departmentId: number;

    jobId: number;
}