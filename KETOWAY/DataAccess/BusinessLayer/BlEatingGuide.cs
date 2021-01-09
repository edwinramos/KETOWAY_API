using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlEatingGuide
    {
        public static List<DeEatingGuide> GetAll()
        {
            return new DlEatingGuide().GetAll();
        }
        public static DeEatingGuide GetByID(int id, string langCode)
        {
            return new DlEatingGuide().GetByID(id, langCode);
        }
        public static DeEatingGuide Save(DeEatingGuide obj)
        {
            return new DlEatingGuide().Save(obj);
        }
        public static void Delete(int id)
        {
            new DlEatingGuide().Delete(id);
        }
    }
}
