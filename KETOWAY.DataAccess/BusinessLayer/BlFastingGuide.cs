using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlFastingGuide
    {
        public static ApiResponse GetAll()
        {
            var result = new ApiResponse();
            try
            {
                var list = new DlFastingGuide().GetAll().Where(x => x.LangCode == "es");
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
                var list = new List<DeFastingGuide>();
                foreach (var item in languages)
                {
                    var obj = new DlFastingGuide().GetAll().FirstOrDefault(x => x.FastingCode == code && x.LangCode == item.LangCode);
                    if (obj != null)
                        list.Add(obj);
                    else
                        list.Add(new DeFastingGuide { FastingCode = code, FastingTitle = "", FastingContent = "", LangCode = item.LangCode });
                }

                result = new ApiResponse(true, "", list);
            }
            catch (Exception ex)
            {
                result = new ApiResponse(false, ex.Message, null);
            }
            return result;
        }
        public static DeFastingGuide GetByCode(string code, string langCode)
        {
            return new DlFastingGuide().GetByCode(code, langCode);
        }
        public static ApiResponse Save(List<DeFastingGuide> model)
        {
            var result = new ApiResponse();
            var dl = new DlFastingGuide();
            try
            {
                var recipeCode = "0";
                if (model.FirstOrDefault().FastingCode == "0")
                {
                    var nextCode = Convert.ToInt32(dl.GetAll().Max(x => x.FastingCode)) + 1;
                    recipeCode = nextCode.ToString().PadLeft(4, '0');
                }
                else
                    recipeCode = model.FirstOrDefault().FastingCode;

                foreach (var obj in model)
                {
                    obj.UpdateDateTime = DateTime.Now;
                    obj.FastingCode = recipeCode;
                    var dbObj = dl.GetByCode(obj.FastingCode, obj.LangCode);
                    if (dbObj != null && dbObj.FastingContent != obj.FastingContent)
                        obj.UpdateDateTime = dbObj.UpdateDateTime;

                    recipeCode = dl.Save(obj).FastingCode;
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
                new DlFastingGuide().Delete(code);
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
