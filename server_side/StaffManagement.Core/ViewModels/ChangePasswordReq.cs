namespace StaffManagement.Core.ViewModels
{
    public class ChangePasswordReq
    {
        public long UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RePassword { get; set; }
    }
}
