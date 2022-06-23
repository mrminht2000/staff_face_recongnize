using StaffManagement.Core.Persistence.Models;
using System;
using System.Linq.Expressions;

namespace StaffManagement.Core.Common
{
    public class UserParams
    {
        public Expression<Func<User, bool>> Filters { get; private set; }

        public UserParams(Expression<Func<User, bool>> filters)
        {
            Filters = filters;
        }
    }
}
