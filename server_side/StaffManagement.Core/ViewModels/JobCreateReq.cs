namespace StaffManagement.Core.ViewModels
{
    public class JobCreateReq
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long Salary { get; set; }

        public char SalaryPer { get; set; }
    }
}
