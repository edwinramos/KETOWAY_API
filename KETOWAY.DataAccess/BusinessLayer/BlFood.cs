using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlFood
    {
        public static List<DeFood> GetAllFood()
        {
            var list = new DlFood().GetAll().ToList();
            return list;
        }
        public static ApiResponse GetAll(bool isAllowed)
        {
            var result = new ApiResponse();
            try
            {
                var list = new DlFood().GetAll().Where(x => x.LangCode == "es" && x.IsAllowed == isAllowed);
                result = new ApiResponse(true, "", list);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static ApiResponse GetByCode(string id)
        {
            var result = new ApiResponse();
            try
            {
                var languages = BlAppLanguage.GetAll();
                var list = new List<DeFood>();
                foreach (var item in languages)
                {
                    var obj = new DlFood().GetAll().FirstOrDefault(x => x.FoodCode == id && x.LangCode == item.LangCode);
                    if (obj != null)
                        list.Add(obj);
                    else
                        list.Add(new DeFood { FoodCode = id, FoodTitle = "", FoodContent = "", FoodGroupID = 1, LangCode = item.LangCode });
                }
                result = new ApiResponse(true, "", list);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static DeFood GetByCode(string code, string langCode)
        {
            return new DlFood().GetByCode(code, langCode);
        }
        public static ApiResponse Save(List<DeFood> model)
        {
            var result = new ApiResponse();
            var dl = new DlFood();
            try
            {
                var recipeCode = "0";
                if (model.FirstOrDefault().FoodCode == "0")
                {
                    var nextCode = Convert.ToInt32(dl.GetAll().Max(x => x.FoodCode)) + 1;
                    recipeCode = nextCode.ToString().PadLeft(4, '0');
                }
                else
                    recipeCode = model.FirstOrDefault().FoodCode;

                foreach (var obj in model)
                {
                    obj.UpdateDateTime = DateTime.Now;
                    obj.FoodCode = recipeCode;
                    var dbObj = dl.GetByCode(obj.FoodCode, obj.LangCode);
                    if (dbObj != null && dbObj.FoodContent != obj.FoodContent)
                        obj.UpdateDateTime = dbObj.UpdateDateTime;

                    recipeCode = dl.Save(obj).FoodCode;
                }
                result = new ApiResponse(true, "", 1);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }

            return result;
        }
        public static ApiResponse Delete(string code)
        {
            var result = new ApiResponse();
            try
            {
                new DlFood().Delete(code);
                result = new ApiResponse(true, "", 1);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
    }
}
