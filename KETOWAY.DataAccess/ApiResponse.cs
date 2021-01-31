using System;

namespace KETOWAY.DataAccess
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }
        public ApiResponse(bool success, string message, object payload)
        {
            Success = success;
            Message = message;
            Payload = payload;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }
    }
}
