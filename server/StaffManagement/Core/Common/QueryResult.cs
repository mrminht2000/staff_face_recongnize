using System.Collections.Generic;

namespace StaffManagement.Core.Common
{
    public class QueryResult<TModel>
    {
        public List<TModel> Data { get; private set; }

        public QueryResult (List<TModel> data)
        {
            Data = data;
        }
    }
}
