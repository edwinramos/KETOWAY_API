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
    public class RecipeController : Controller
    {
        [HttpGet]
        public ApiResponse GetAllRecipes()
        {
            var result = BlRecipe.GetAll();
            return result;
        }

        [HttpGet("{id}")]
        public ApiResponse GetRecipes(string id)
        {
            var result = BlRecipe.GetRecipeAllLanguages(id);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse> PostRecipe([FromBody] ApiRequest model)
        {
            var obj = JsonConvert.DeserializeObject<List<DeRecipe>>(model.Payload.ToString());
            var result = BlRecipe.Save(obj);
            return result;
        }

        [HttpDelete("{code}")]
        public async Task<ApiResponse> DeleteRecipe(string code)
        {
            var result = BlRecipe.Delete(code);
            return result;
        }
    }
}
