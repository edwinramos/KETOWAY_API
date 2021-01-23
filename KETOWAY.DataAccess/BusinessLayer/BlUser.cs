using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlUser
    {
        public static List<DeUser> GetAll()
        {
            return new DlUser().GetAll();
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
                    result = new ApiResponse(false, "Invalid User", null);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static DeUser GetByCode(string code)
        {
            return new DlUser().GetByCode(code);
        }
        public static DeUser Save(DeUser obj)
        {
            return new DlUser().Save(obj);
        }
        public static void Delete(string code)
        {
            new DlUser().Delete(code);
        }
    }
}
