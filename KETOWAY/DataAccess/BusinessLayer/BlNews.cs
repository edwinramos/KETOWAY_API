using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlNews
    {
        public static List<DeNews> GetAll()
        {
            return new DlNews().GetAll();
        }
        public static DeNews GetByCode(string code, string langCode)
        {
            return new DlNews().GetByCode(code, langCode);
        }
        public static DeNews Save(DeNews obj)
        {
            return new DlNews().Save(obj);
        }
        public static void Delete(string code)
        {
            new DlNews().Delete(code);
        }
    }
}
