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
    public class FastingGuideController : Controller
    {
        [HttpGet]
        public ApiResponse GetAllFastingGuides()
        {
            var result = BlFastingGuide.GetAll();
            return result;
        }

        [HttpGet("{id}")]
        public ApiResponse GetFastingGuides(string id)
        {
            var result = BlFastingGuide.GetByCode(id);
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public ApiResponse PostFastingGuide([FromBody] ApiRequest model)
        {
            var obj = JsonConvert.DeserializeObject<List<DeFastingGuide>>(model.Payload.ToString());
            var result = BlFastingGuide.Save(obj);
            return result;
        }

        [HttpDelete("{code}")]
        public ApiResponse DeleteFastingGuide(string code)
        {
            var result = BlFastingGuide.Delete(code);
            return result;
        }
    }
}
