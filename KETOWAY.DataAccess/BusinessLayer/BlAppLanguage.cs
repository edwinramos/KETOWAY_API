using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlAppLanguage
    {
        public static List<DeAppLanguage> GetAll()
        {
            return new DlAppLanguage().GetAll();
        }
        public static DeAppLanguage GetByCode(string code)
        {
            return new DlAppLanguage().GetByCode(code);
        }
        public static DeAppLanguage Save(DeAppLanguage obj)
        {
            return new DlAppLanguage().Save(obj);
        }
    }
}
