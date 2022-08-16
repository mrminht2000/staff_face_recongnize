using StaffManagement.API.Core.Services.Dtos;
using System.Collections.Generic;

namespace StaffManagement.API.ViewModels
{
    public class UserQueryResp
    {
        public List<UserData> Users { get; set; }
    }
}
