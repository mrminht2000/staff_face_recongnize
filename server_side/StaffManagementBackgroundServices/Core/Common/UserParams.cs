﻿using StaffManagement.BackgroundServices.Core.Persistence.Models;
using System;
using System.Linq.Expressions;

namespace StaffManagement.BackgroundServices.Core.Common
{
    public class UserParams
    {
        public Expression<Func<User, bool>> Filters { get; private set; }

        public UserParams(Expression<Func<User, bool>> filters)
        {
            Filters = filters;
        }

        public UserParams()
        {
            Filters = null;
        }
    }
}