using KetoWay.DataAccess.DataEntities;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlRecipe
    {
        public static List<DeRecipe> GetAll()
        {
            return new DlRecipe().GetAll();
        }
        public static DeRecipe GetByCode(string code, string langCode)
        {
            return new DlRecipe().GetByCode(code, langCode);
        }
        public static DeRecipe Save(DeRecipe obj)
        {
            return new DlRecipe().Save(obj);
        }
        public static void Delete(string code)
        {
            new DlRecipe().Delete(code);
        }
    }
}
