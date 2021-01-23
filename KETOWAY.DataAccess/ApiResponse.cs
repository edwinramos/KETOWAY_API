using System;

namespace KETOWAY.DataAccess
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }
        public ApiResponse(bool success, string message, object body)
        {
            Success = success;
            Message = message;
            Body = body;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Body { get; set; }
    }
}
