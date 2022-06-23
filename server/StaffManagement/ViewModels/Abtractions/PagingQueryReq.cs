namespace StaffManagement.ViewModels
{
    public abstract class PagingQueryReq
    {
        public string SortField { get; set; }

        public string SortDirection { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
