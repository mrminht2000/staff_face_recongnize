using StaffManagement.Core.Persistence.Models;
using System.Collections.Generic;

namespace StaffManagement.Core.Common
{
    public class UserResult
    {
        public List<User> Users { get; private set; }

        public UserResult(List<User> users)
        {
            Users = users;
        }
    }
}
