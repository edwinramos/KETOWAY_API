using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlRecipe
    {
        public static ApiResponse GetAll()
        {
            var result = new ApiResponse();
            try
            {
                var list = new DlRecipe().GetAll().Where(x => x.LangCode == "es");
                result = new ApiResponse(true, "", list);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static ApiResponse GetRecipeAllLanguages(string id)
        {
            var result = new ApiResponse();
            try
            {
                var languages = BlAppLanguage.GetAll();
                var list = new List<DeRecipe>();
                foreach (var item in languages)
                {
                    var obj = new DlRecipe().GetAll().FirstOrDefault(x => x.RecipeCode == id && x.LangCode == item.LangCode);
                    if (obj != null)
                        list.Add(obj);
                    else
                        list.Add(new DeRecipe { RecipeCode = id, RecipeTitle = "", RecipeContent = "", LangCode = item.LangCode });
                }
                result = new ApiResponse(true, "", list);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static ApiResponse Save(List<DeRecipe> model)
        {
            var result = new ApiResponse();
            var dl = new DlRecipe();
            try
            {
                var recipeCode = "0";
                if (model.FirstOrDefault().RecipeCode == "0")
                {
                    var nextCode = Convert.ToInt32(dl.GetAll().Max(x => x.RecipeCode)) + 1;
                    recipeCode = nextCode.ToString().PadLeft(4, '0');
                }
                else
                    recipeCode = model.FirstOrDefault().RecipeCode;

                foreach (var obj in model)
                {
                    obj.UpdateDateTime = DateTime.Now;
                    obj.RecipeCode = recipeCode;
                    var dbObj = dl.GetByCode(obj.RecipeCode, obj.LangCode);
                    if (dbObj != null && dbObj.RecipeContent != obj.RecipeContent)
                        obj.UpdateDateTime = dbObj.UpdateDateTime;

                    recipeCode = dl.Save(obj).RecipeCode;
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
                new DlRecipe().Delete(code);
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
