using System;

namespace StaffManagement.ViewModels
{
    public class AuthUserResp
    {
        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
