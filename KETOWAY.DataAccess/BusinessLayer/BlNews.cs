using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlNews
    {
        public static ApiResponse GetAll()
        {
            var result = new ApiResponse();
            try
            {
                var list = new DlNews().GetAll().Where(x => x.LangCode == "es");
                result = new ApiResponse(true, "", list);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static ApiResponse GetByCode(string code)
        {
            var result = new ApiResponse();
            try
            {
                var languages = BlAppLanguage.GetAll();
                var list = new List<DeNews>();
                foreach (var item in languages)
                {
                    var obj = new DlNews().GetAll().FirstOrDefault(x => x.NewsCode == code && x.LangCode == item.LangCode);
                    if (obj != null)
                        list.Add(obj);
                    else
                        list.Add(new DeNews { NewsCode = code, NewsTitle = "", NewsContent = "", LangCode = item.LangCode });
                }

                result = new ApiResponse(true, "", list);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static DeNews GetByCode(string code, string langCode)
        {
            return new DlNews().GetByCode(code, langCode);
        }
        public static ApiResponse Save(List<DeNews> model)
        {
            var result = new ApiResponse();
            var dl = new DlNews();
            try
            {
                var recipeCode = "0";
                if (model.FirstOrDefault().NewsCode == "0")
                {
                    var nextCode = Convert.ToInt32(dl.GetAll().Max(x => x.NewsCode)) + 1;
                    recipeCode = nextCode.ToString().PadLeft(4, '0');
                }
                else
                    recipeCode = model.FirstOrDefault().NewsCode;

                foreach (var obj in model)
                {
                    obj.NewsCode = recipeCode;
                    var dbObj = dl.GetByCode(obj.NewsCode, obj.LangCode);
                    if (dbObj != null)
                        obj.UpdateDateTime = dbObj.UpdateDateTime;

                    recipeCode = dl.Save(obj).NewsCode;
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
                new DlNews().Delete(code);
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
