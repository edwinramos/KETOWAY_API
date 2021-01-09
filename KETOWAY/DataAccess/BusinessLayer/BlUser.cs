using KetoWay.DataAccess.DataEntities;
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
