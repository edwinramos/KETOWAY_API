using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlEatingGuideDetail
    {
        public static List<DeEatingGuideDetail> GetAll()
        {
            return new DlEatingGuideDetail().GetAll();
        }
        public static DeEatingGuideDetail GetByID(int id, string foodCode, string langCode)
        {
            return new DlEatingGuideDetail().GetByID(id, foodCode, langCode);
        }
        public static DeEatingGuideDetail Save(DeEatingGuideDetail obj)
        {
            return new DlEatingGuideDetail().Save(obj);
        }
        public static void Delete(int id, string foodCode)
        {
            new DlEatingGuideDetail().Delete(id, foodCode);
        }
    }
}
