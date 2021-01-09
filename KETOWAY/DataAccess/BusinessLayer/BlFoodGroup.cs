using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlFoodGroup
    {
        public static List<DeFoodGroup> GetAll()
        {
            return new DlFoodGroup().GetAll();
        }
        public static DeFoodGroup GetByID(int id)
        {
            return new DlFoodGroup().GetByID(id);
        }
        public static DeFoodGroup Save(DeFoodGroup obj)
        {
            return new DlFoodGroup().Save(obj);
        }
    }
}
