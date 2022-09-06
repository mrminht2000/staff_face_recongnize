using System;

namespace StaffManagement.Core.ViewModels
{
    public class AuthUserResp
    {
        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
