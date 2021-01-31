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
    public class FoodController : Controller
    {
        [HttpGet("getFoods/{isAllowed}")]
        public ApiResponse GetAllFood(bool isAllowed)
        {
            var result = BlFood.GetAll(isAllowed);
            return result;
        }

        [HttpGet("{id}")]
        public ApiResponse GetFood(string id)
        {
            var result = BlFood.GetByCode(id);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse> PostFood([FromBody] ApiResponse model)
        {
            var obj = JsonConvert.DeserializeObject<List<DeFood>>(model.Payload.ToString());
            var result = BlFood.Save(obj);
            return result;
        }

        [HttpDelete("{code}")]
        public async Task<ApiResponse> DeleteFood(string code)
        {
            var result = BlFood.Delete(code);
            return result;
        }
    }
}
