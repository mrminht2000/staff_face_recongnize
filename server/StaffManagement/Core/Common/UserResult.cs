using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Services.Dtos;
using System.Collections.Generic;

namespace StaffManagement.Core.Common
{
    public class UserResult
    {
        public List<UserData> Users { get; private set; }

        public UserData User { get; private set; }

        public UserResult(List<User> users)
        {
            Users = users.ConvertAll(user => new UserData(user));
        }
    }
}
