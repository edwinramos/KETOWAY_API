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
    //[Route("api")]
    public class NewsController : Controller
    {
        [HttpGet]
        public ApiResponse GetAllNews()
        {
            var result = BlNews.GetAll();
            return result;
        }

        [HttpGet("{id}")]
        public ApiResponse GetNews(string id)
        {
            var result = BlNews.GetByCode(id);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse> PostNews([FromBody] ApiRequest model)
        {
            var obj = JsonConvert.DeserializeObject<List<DeNews>>(model.Payload.ToString());
            var result = BlNews.Save(obj);
            return result;
        }

        [HttpDelete("{code}")]
        public async Task<ApiResponse> DeleteNews(string code)
        {
            var result = BlNews.Delete(code);
            return result;
        }
    }
}
