using System;
using System.Linq.Expressions;

namespace StaffManagement.Core.Common
{
    public class QueryParams<TModel>
    {
        public Expression<Func<TModel, bool>> Filters { get; private set; }

        public QueryParams(Expression<Func<TModel, bool>> filters)
        {
            Filters = filters;
        }
    }
}
