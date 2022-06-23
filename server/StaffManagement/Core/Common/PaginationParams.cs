using System;
using System.Linq.Expressions;

namespace StaffManagement.Core.Common
{
    public class PaginationParams<TModel>
    {
        public Expression<Func<TModel, bool>> Filters { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public string SortField { get; private set; }

        public string SortDirection { get; private set; }

        public PaginationParams(Expression<Func<TModel, bool>> filters, string sortField, string sortDirection, int skip, int take)
        {
            Filters = filters;
            SortDirection = sortDirection;
            SortField = sortField;
            Skip = skip;
            Take = take == 0 ? 50 : take;
        }

        public PaginationParams(string sortField, string sortDirection, int skip, int take)
            : this(null, sortField, sortDirection, skip, take)
        { }

        public PaginationParams(int skip, int take)
            : this(null, null, null, skip, take)
        { }
    }
}
