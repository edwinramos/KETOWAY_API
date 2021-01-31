using Microsoft.AspNetCore.Http;
using System;

namespace KETOWAY.Utilities.Helpers
{
    public static class CookieHelper
    {
        public static void Create(HttpRequest Request, HttpResponse Response, string key, string value, DateTime expireDate)
        {
            string activeUser = Request.Cookies[key];

            if (string.IsNullOrEmpty(activeUser))
            {
                CookieOptions option = new CookieOptions { Expires = expireDate};

                Response.Cookies.Append("activeUser", value, option);
            }
        }

        public static void Remove(HttpResponse Response, string key)
        {
            Response.Cookies.Delete(key);
        }

        public static string Read(HttpRequest Request, string key)
        {
            return Request.Cookies[key];
        }
    }
}
