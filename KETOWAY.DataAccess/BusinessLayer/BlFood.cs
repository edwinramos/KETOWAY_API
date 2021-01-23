using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlFood
    {
        public static List<DeFood> GetAll()
        {
            return new DlFood().GetAll();
        }
        public static DeFood GetByCode(string code, string langCode)
        {
            return new DlFood().GetByCode(code, langCode);
        }
        public static DeFood Save(DeFood obj)
        {
            return new DlFood().Save(obj);
        }
        public static void Delete(string code)
        {
            new DlFood().Delete(code);
        }
    }
}
