namespace StaffManagement.Core.Core.Services.Dtos
{
    public class ChangePasswordRequest
    {
        public long UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RePassword { get; set; }
    }
}
