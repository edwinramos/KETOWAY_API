using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KetoWayApi.DataAccess.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KetoWayApi.DataAccess.BusinessLayer
{
    public static class BlAppInfo
    {
        public static ApiResponse GetAppInfo()
        {
            var result = new ApiResponse();
            var dl = new DlAppInfo();
            try
            {
                var list = new List<DeAppInfo>();
                list.AddRange(dl.GetByCode("about"));
                list.AddRange(dl.GetByCode("reference"));

                result = new ApiResponse() { Success = true, Payload = list };
            }
            catch (Exception ex)
            {
                result = new ApiResponse() { Success = false, Payload = null, Message = ex.Message };
            }

            return result;
        }
        public static ApiResponse GetData(string code)
        {
            var result = new ApiResponse();
            var dl = new DlAppInfo();
            try
            {
                var list = new List<DeAppInfo>();
                list.AddRange(dl.GetByCode("about"));
                list.AddRange(dl.GetByCode("reference"));

                result = new ApiResponse() { Success = true, Payload = list };
            }
            catch (Exception ex)
            {
                result = new ApiResponse() { Success = false, Payload = null, Message = ex.Message };
            }
            return result;
        }
        public static ApiResponse Save(DeAppInfo model)
        {
            var result = new ApiResponse();
            var dl = new DlAppInfo();
            try
            {
                var obj = dl.GetByCode(model.InfoCode).FirstOrDefault(x => x.LangCode == model.LangCode);
                if (obj != null)
                {
                    model.UpdateDateTime = DateTime.Now;
                    if (obj.InfoContent != model.InfoContent)
                    {
                        obj.InfoContent = model.InfoContent;
                        obj.UpdateDateTime = model.UpdateDateTime;
                    }
                    obj = dl.Save(obj);

                    result = new ApiResponse() { Success = true, Payload = obj };
                }
                else
                    result = new ApiResponse() { Success = false, Payload = null, Message = "Not valid Info" };
            }
            catch (Exception ex)
            {
                result = new ApiResponse() { Success = false, Payload = null, Message = ex.Message };
            }
            return result;
        }
        public static ApiResponse GetInfoByCode(string code)
        {
            var result = new ApiResponse();
            var dl = new DlAppInfo();
            try
            {
                var languages = BlAppLanguage.GetAll();
                var list = new List<DeAppInfo>();
                foreach (var item in languages)
                {
                    var obj = dl.GetAll().FirstOrDefault(x => x.InfoCode == code && x.LangCode == item.LangCode);
                    if (obj != null)
                        list.Add(obj);
                    else
                        list.Add(new DeAppInfo { InfoCode = code, InfoContent = "", LangCode = item.LangCode });
                }
                result = new ApiResponse() { Success = true, Payload = list };
            }
            catch (Exception ex)
            {
                result = new ApiResponse() { Success = false, Payload = null, Message = ex.Message };
            }
            return result;
        }
        //public static List<DeAppInfo> GetByCode(string code)
        //{
        //    return new DlAppInfo().GetByCode(code);
        //}
        //public static DeAppInfo Save(DeAppInfo obj)
        //{
        //    return new DlAppInfo().Save(obj);
        //}
    }
}
