using System.Collections.Generic;

namespace StaffManagement.Core.Common
{
    public class PaginationResult<TModel>
    {
        public List<TModel> Data { get; private set; }

        public int Total { get; private set; }

        public PaginationResult(List<TModel> data, int total)
        {
            Data = data;
            Total = total;
        }
    }
}
