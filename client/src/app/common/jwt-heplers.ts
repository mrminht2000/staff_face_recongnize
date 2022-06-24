import { User } from "../models/user/user.model";

export const  parseJwt = (token: string): User => {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join('')
    );

    var obj = JSON.parse(jsonPayload);

    return {
        id: obj.user_id,
        userName: obj.user_name,
        fullName: obj.full_name,
        role: obj.role
    } as User;
}
