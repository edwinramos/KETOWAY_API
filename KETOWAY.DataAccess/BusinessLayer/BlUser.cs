using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KETOWAY.Utitlities.Helpers;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlUser
    {
        public static ApiResponse GetAll()
        {
            var result = new ApiResponse();
            try
            {
                var users = new DlUser().GetAll();
                result = new ApiResponse(true, "", users);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static ApiResponse IsValidUser(string code, string password)
        {
            var result = new ApiResponse();
            try
            {
                var usr = new DlUser().GetByCode(code);
                if (usr != null && usr.Password == password)
                    result = new ApiResponse(true, "", usr);
                else
                    result = new ApiResponse(false, "😥 Invalid User", null);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static ApiResponse GetByCode(string code)
        {
            var result = new ApiResponse();
            try
            {
                var usr = new DlUser().GetByCode(code);
                if (usr != null)
                    result = new ApiResponse(true, "", usr);
                else
                {
                    usr = new DeUser
                    {
                        UserCode = "",
                        Name = "",
                        LastName = "",
                        Email = "",
                        Password = "",
                        ImagePath = "",
                        BirthDate = DateTime.Today
                    };
                }
                result = new ApiResponse(true, "New User", usr);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static ApiResponse Save(DeUser obj)
        {
            var result = new ApiResponse();
            try
            {
                new DlUser().Save(obj);
                result = new ApiResponse(true, "", 1);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static ApiResponse Delete(string code)
        {
            var result = new ApiResponse();
            try
            {
                new DlUser().Delete(code);
                result = new ApiResponse(true, "", 1);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, "Error Deleting");
            }

            return result;
        }
        public static ApiResponse ForgotPassword(string email)
        {
            var result = new ApiResponse();
            var dl = new DlUser();
            try
            {
                var obj = dl.GetAll().FirstOrDefault(x => x.Email == email);

                if (obj != null)
                {
                    if (MailHelper.ForgotPasswordMail(email, obj.UserCode))
                        result = new ApiResponse(true, "", 1);
                    else
                        result = new ApiResponse(false, "Error sending mail. Please contact administrator.", null);
                }
                else
                    result = new ApiResponse(false, "Email not registered, please try again", null);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, "Error");
            }

            return result;
        }
        public static ApiResponse RecoverPassword(string userCode, string password)
        {
            var result = new ApiResponse();
            var dl = new DlUser();
            try
            {
                var obj = dl.GetByCode(userCode);

                if (obj != null)
                {
                    obj.Password = password;
                    dl.Save(obj);
                }
                result = new ApiResponse(true, "", 1);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, "Error recovering password");
            }

            return result;
        }
    }
}
