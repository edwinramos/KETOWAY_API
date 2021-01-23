using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlSection
    {
        public static List<DeSection> GetAll()
        {
            return new DlSection().GetAll();
        }
        public static DeSection GetByID(int id)
        {
            return new DlSection().GetByID(id);
        }
        public static DeSection Save(DeSection obj)
        {
            return new DlSection().Save(obj);
        }
    }
}
