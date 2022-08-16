using StaffManagement.API.Core.Persistence.Models;
using StaffManagement.API.Core.Services.Dtos;
using System.Collections.Generic;

namespace StaffManagement.API.Core.Common
{
    public class UserResult
    {
        public List<UserData> Users { get; private set; }

        public UserData User { get; private set; }

        public UserResult(List<User> users)
        {
            Users = users.ConvertAll(user => new UserData(user));
        }

        public UserResult(List<UserData> users)
        {
            Users = users;
        }
    }
}
