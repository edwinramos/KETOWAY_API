using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlFastingGuide
    {
        public static List<DeFastingGuide> GetAll()
        {
            return new DlFastingGuide().GetAll();
        }
        public static DeFastingGuide GetByCode(string code, string langCode)
        {
            return new DlFastingGuide().GetByCode(code, langCode);
        }
        public static DeFastingGuide Save(DeFastingGuide obj)
        {
            return new DlFastingGuide().Save(obj);
        }
        public static void Delete(string code)
        {
            new DlFastingGuide().Delete(code);
        }
    }
}
