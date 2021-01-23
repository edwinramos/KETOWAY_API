using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlMeasurementUnit
    {
        public static List<DeMeasurementUnit> GetAll()
        {
            return new DlMeasurementUnit().GetAll();
        }
        public static DeMeasurementUnit GetByCode(string code)
        {
            return new DlMeasurementUnit().GetByCode(code);
        }
        public static DeMeasurementUnit Save(DeMeasurementUnit obj)
        {
            return new DlMeasurementUnit().Save(obj);
        }
    }
}
