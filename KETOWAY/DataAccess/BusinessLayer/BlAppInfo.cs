using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlAppInfo
    {
        public static List<DeAppInfo> GetAll()
        {
            return new DlAppInfo().GetAll();
        }
        public static List<DeAppInfo> GetByCode(string code)
        {
            return new DlAppInfo().GetByCode(code);
        }
        public static DeAppInfo Save(DeAppInfo obj)
        {
            return new DlAppInfo().Save(obj);
        }
    }
}
