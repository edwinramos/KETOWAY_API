using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KetoWayApi.DataAccess.BusinessLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KETOWAY.Controllers
{
    [Route("api/[controller]")]
    public class AppInfoController : Controller
    {
        [HttpGet]
        public ApiResponse GetAppInfo()
        {
            var result = BlAppInfo.GetAppInfo();
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] ApiRequest model)
        {
            var obj = JsonConvert.DeserializeObject<DeAppInfo>(model.Body.ToString());
            var result = BlAppInfo.Save(obj);
            return result;
        }

        [HttpGet("getInfo/{id}")]
        public ApiResponse GetInfo(string id)
        {
            var result = BlAppInfo.GetInfoByCode(id);
            return result;
        }
    }
}
