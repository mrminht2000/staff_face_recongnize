using Newtonsoft.Json;

namespace StaffManagement.Core.Services.Dtos
{
    public class ErrorRespond
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
