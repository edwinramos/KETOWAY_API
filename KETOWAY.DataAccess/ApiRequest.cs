using System;

namespace KETOWAY.DataAccess
{
    public class ApiRequest
    {
        public object Payload { get; set; }
        public string UserCode { get; set; }
        public string Token { get; set; }
    }
}
