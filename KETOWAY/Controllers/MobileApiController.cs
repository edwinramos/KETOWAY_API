using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KetoWay.DataAccess.DataEntities;
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
    public class MobileApiController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        [Route("test")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #region AppInfo

        [HttpGet]
        [Route("appInfo")]
        public JsonResult GetAppInfo()
        {
            var result = new List<DeAppInfo>();
            result.AddRange(BlAppInfo.GetByCode("about"));
            result.AddRange(BlAppInfo.GetByCode("reference"));

            return Json(result);
        }

        // POST api/<controller>
        [HttpPost("postInfo")]
        public async Task<IActionResult> Post([FromBody] DeAppInfo obj)
        {
            obj.UpdateDateTime = DateTime.Now;

            try
            {
                var dbObj = BlAppInfo.GetByCode(obj.InfoCode).FirstOrDefault(x => x.LangCode == obj.LangCode);
                if (dbObj != null && dbObj.InfoContent != obj.InfoContent)
                    obj.UpdateDateTime = dbObj.UpdateDateTime;

                obj = BlAppInfo.Save(obj);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getInfo/{id}")]
        public JsonResult GetInfo(string id)
        {
            var languages = BlAppLanguage.GetAll();
            var result = new List<DeAppInfo>();
            //BlNew.GetAll().Where(x => x.NewCode == id)
            foreach (var item in languages)
            {
                var obj = BlAppInfo.GetAll().FirstOrDefault(x => x.InfoCode == id && x.LangCode == item.LangCode);
                if (obj != null)
                    result.Add(obj);
                else
                    result.Add(new DeAppInfo { InfoCode = id, InfoContent = "", LangCode = item.LangCode });
            }

            return Json(result);
        }
        #endregion

        #region Recipes

        [HttpGet]
        [Route("getRecipes")]
        public JsonResult GetAllRecipes()
        {
            var result = BlRecipe.GetAll().Where(x => x.LangCode == "es");

            return Json(result);
        }

        //[HttpGet("{id}")]
        [Route("getRecipe/{id}")]
        public JsonResult GetRecipes(string id)
        {
            var languages = BlAppLanguage.GetAll();
            var result = new List<DeRecipe>();
            //BlRecipe.GetAll().Where(x => x.RecipeCode == id)
            foreach (var item in languages)
            {
                var obj = BlRecipe.GetAll().FirstOrDefault(x => x.RecipeCode == id && x.LangCode == item.LangCode);
                if (obj != null)
                    result.Add(obj);
                else
                    result.Add(new DeRecipe { RecipeCode = id, RecipeTitle = "", RecipeContent = "", LangCode = item.LangCode });
            }
            return Json(result);
        }

        // POST api/<controller>
        [HttpPost("postRecipe")]
        public async Task<IActionResult> PostRecipe([FromBody] DeRecipe[] objList)
        {
            try
            {
                var recipeCode = "0";
                if (objList.FirstOrDefault().RecipeCode == "0")
                {
                    var nextCode = Convert.ToInt32(BlRecipe.GetAll().Max(x => x.RecipeCode)) + 1;
                    recipeCode = nextCode.ToString().PadLeft(4, '0');
                }
                else
                    recipeCode = objList.FirstOrDefault().RecipeCode;

                foreach (var obj in objList)
                {
                    obj.UpdateDateTime = DateTime.Now;
                    obj.RecipeCode = recipeCode;
                    var dbObj = BlRecipe.GetByCode(obj.RecipeCode, obj.LangCode);
                    if (dbObj != null && dbObj.RecipeContent != obj.RecipeContent)
                        obj.UpdateDateTime = dbObj.UpdateDateTime;

                    recipeCode = BlRecipe.Save(obj).RecipeCode;
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteRecipe")]
        public async Task<IActionResult> DeleteRecipe([FromBody] string code)
        {
            try
            {
                BlRecipe.Delete(code);
            }
            catch (Exception ex)
            {
                return BadRequest("");
            }
            return Ok("Success");
        }

        #endregion

        #region Food

        [HttpGet]
        [Route("getFoods/{isAllowed}")]
        public JsonResult GetAllFood(bool isAllowed)
        {
            var result = BlFood.GetAll().Where(x => x.LangCode == "es" && x.IsAllowed == isAllowed);

            return Json(result);
        }

        //[HttpGet("{id}")]
        [Route("getFood/{id}")]
        public JsonResult GetFood(string id)
        {
            var languages = BlAppLanguage.GetAll();
            var result = new List<DeFood>();
            //BlRecipe.GetAll().Where(x => x.RecipeCode == id)
            foreach (var item in languages)
            {
                var obj = BlFood.GetAll().FirstOrDefault(x => x.FoodCode == id && x.LangCode == item.LangCode);
                if (obj != null)
                    result.Add(obj);
                else
                    result.Add(new DeFood { FoodCode = id, FoodTitle = "", FoodContent = "", FoodGroupID = 1, LangCode = item.LangCode });
            }
            return Json(result);
        }

        // POST api/<controller>
        [HttpPost("postFood")]
        public async Task<IActionResult> PostFood([FromBody] DeFood[] objList)
        {
            try
            {
                var recipeCode = "0";
                if (objList.FirstOrDefault().FoodCode == "0")
                {
                    var nextCode = Convert.ToInt32(BlFood.GetAll().Max(x => x.FoodCode)) + 1;
                    recipeCode = nextCode.ToString().PadLeft(4, '0');
                }
                else
                    recipeCode = objList.FirstOrDefault().FoodCode;

                foreach (var obj in objList)
                {
                    obj.UpdateDateTime = DateTime.Now;
                    obj.FoodCode = recipeCode;
                    var dbObj = BlFood.GetByCode(obj.FoodCode, obj.LangCode);
                    if (dbObj != null && dbObj.FoodContent != obj.FoodContent)
                        obj.UpdateDateTime = dbObj.UpdateDateTime;

                    recipeCode = BlFood.Save(obj).FoodCode;
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteFood")]
        public async Task<IActionResult> DeleteFood([FromBody] string code)
        {
            try
            {
                BlFood.Delete(code);
            }
            catch (Exception ex)
            {
                return BadRequest("");
            }
            return Ok("Success");
        }

        #endregion

        #region FastingGuides

        [HttpGet]
        [Route("getFastingGuides")]
        public JsonResult GetAllFastingGuides()
        {
            var result = BlFastingGuide.GetAll().Where(x => x.LangCode == "es");

            return Json(result);
        }

        //[HttpGet("{id}")]
        [Route("getFastingGuide/{id}")]
        public JsonResult GetFastingGuides(string id)
        {
            var languages = BlAppLanguage.GetAll();
            var result = new List<DeFastingGuide>();
            //BlFastingGuide.GetAll().Where(x => x.FastingCode == id)
            foreach (var item in languages)
            {
                var obj = BlFastingGuide.GetAll().FirstOrDefault(x => x.FastingCode == id && x.LangCode == item.LangCode);
                if (obj != null)
                    result.Add(obj);
                else
                    result.Add(new DeFastingGuide { FastingCode = id, FastingTitle = "", FastingContent = "", LangCode = item.LangCode });
            }
            return Json(result);
        }

        // POST api/<controller>
        [HttpPost("postFastingGuide")]
        public async Task<IActionResult> PostFastingGuide([FromBody] DeFastingGuide[] objList)
        {
            try
            {
                var recipeCode = "0";
                if (objList.FirstOrDefault().FastingCode == "0")
                {
                    var nextCode = Convert.ToInt32(BlFastingGuide.GetAll().Max(x => x.FastingCode)) + 1;
                    recipeCode = nextCode.ToString().PadLeft(4, '0');
                }
                else
                    recipeCode = objList.FirstOrDefault().FastingCode;

                foreach (var obj in objList)
                {
                    obj.UpdateDateTime = DateTime.Now;
                    obj.FastingCode = recipeCode;
                    var dbObj = BlFastingGuide.GetByCode(obj.FastingCode, obj.LangCode);
                    if (dbObj != null && dbObj.FastingContent != obj.FastingContent)
                        obj.UpdateDateTime = dbObj.UpdateDateTime;

                    recipeCode = BlFastingGuide.Save(obj).FastingCode;
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteFastingGuide")]
        public async Task<IActionResult> DeleteFastingGuide([FromBody] string code)
        {
            try
            {
                BlFastingGuide.Delete(code);
            }
            catch (Exception ex)
            {
                return BadRequest("");
            }
            return Ok("Success");
        }

        #endregion

        #region EatingGuide
        [HttpGet]
        [Route("getEatingGuide")]
        public JsonResult GetEatingGuide()
        {
            foreach (var lang in BlAppLanguage.GetAll())
            {
                if (!BlEatingGuide.GetAll().Any(x => x.LangCode == lang.LangCode))
                {
                    BlEatingGuide.Save(new DeEatingGuide
                    {
                        ID = 1,
                        LangCode = lang.LangCode,
                        EatingGuideTitle = "",
                        EatingGuideContent = "",
                        UpdateDateTime = DateTime.Now
                    });
                }
            }

            var result = BlEatingGuide.GetAll();
            return Json(result);
        }
        [HttpGet]
        [Route("getEatingGuideDetail/{id}")]
        public JsonResult GetEatingGuideDetail(int id, string langCode)
        {
            var result = BlEatingGuideDetail.GetAll().Where(x => x.HeadId == id && x.LangCode == langCode).Select(x => new
            {
                x.HeadId,
                x.FoodCode,
                x.FoodDescription,
                BlFoodGroup.GetAll().FirstOrDefault(p => p.ID == x.FoodGroupID && p.LangCode == langCode).FoodGroupDescription,
                BlSection.GetAll().FirstOrDefault(p => p.ID == x.SectionID && p.LangCode == langCode).SectionDescription,
                x.LangCode,
                x.Quantity_MeasurementUnitCode,
                x.Quantity,
                QuantityDesc = $"{x.Quantity} {x.Quantity_MeasurementUnitCode}",
                x.Calories,
                x.Carbs,
                x.Protein,
                x.Fat
            });
            return Json(result);
        }
        [HttpGet]
        [Route("getEatingGuideDetailData")]
        public JsonResult GetEatingGuideDetailData(string langCode)
        {
            return Json(new
            {
                foodList = BlFood.GetAll().Where(x => x.IsAllowed && x.LangCode == langCode),
                sections = BlSection.GetAll().Where(x => x.LangCode == langCode),
                measurementUnits = BlMeasurementUnit.GetAll()
            });
        }

        [HttpPost("postEatingGuide")]
        public async Task<IActionResult> PostEatingGuide([FromBody] DeEatingGuide[] objList)
        {
            try
            {
                var recipeCode = objList.FirstOrDefault().ID;

                foreach (var obj in objList)
                {
                    if (recipeCode != 0)
                    {
                        obj.UpdateDateTime = DateTime.Now;
                        BlEatingGuide.Save(obj);
                    }
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("postEatingGuideDetail")]
        public async Task<IActionResult> PostEatingGuide([FromBody] DeEatingGuideDetail model)
        {
            try
            {
                model.HeadId = 1;
                model.UpdateDateTime = DateTime.Now;
                model.FoodGroupID = BlFood.GetByCode(model.FoodCode, "es").FoodGroupID;

                foreach (var lang in BlAppLanguage.GetAll())
                {
                    model.LangCode = lang.LangCode;
                    model.FoodDescription = BlFood.GetByCode(model.FoodCode, model.LangCode).FoodTitle;

                    BlEatingGuideDetail.Save(model);
                }

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteEatingGuideDetail")]
        public async Task<IActionResult> DeleteEatingGuide([FromBody] string foodCode)
        {
            try
            {
                BlEatingGuideDetail.Delete(1, foodCode);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region News

        [HttpGet]
        [Route("getAllNews")]
        public JsonResult GetAllNews()
        {
            var result = BlNews.GetAll().Where(x => x.LangCode == "es");

            return Json(result);
        }

        //[HttpGet("{id}")]
        [Route("getNews/{id}")]
        public JsonResult GetNews(string id)
        {
            var languages = BlAppLanguage.GetAll();
            var result = new List<DeNews>();
            //BlNew.GetAll().Where(x => x.NewCode == id)
            foreach (var item in languages)
            {
                var obj = BlNews.GetAll().FirstOrDefault(x => x.NewsCode == id && x.LangCode == item.LangCode);
                if (obj != null)
                    result.Add(obj);
                else
                    result.Add(new DeNews { NewsCode = id, NewsTitle = "", NewsContent = "", LangCode = item.LangCode });
            }
            return Json(result);
        }

        // POST api/<controller>
        [HttpPost("postNews")]
        public async Task<IActionResult> PostNews([FromBody] DeNews[] objList)
        {
            try
            {
                var recipeCode = "0";
                if (objList.FirstOrDefault().NewsCode == "0")
                {
                    var nextCode = Convert.ToInt32(BlNews.GetAll().Max(x => x.NewsCode)) + 1;
                    recipeCode = nextCode.ToString().PadLeft(4, '0');
                }
                else
                    recipeCode = objList.FirstOrDefault().NewsCode;

                foreach (var obj in objList)
                {
                    obj.NewsCode = recipeCode;
                    var dbObj = BlNews.GetByCode(obj.NewsCode, obj.LangCode);
                    if (dbObj != null)
                        obj.UpdateDateTime = dbObj.UpdateDateTime;

                    recipeCode = BlNews.Save(obj).NewsCode;
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteNews")]
        public async Task<IActionResult> DeleteNews([FromBody] string code)
        {
            try
            {
                BlNews.Delete(code);
            }
            catch (Exception ex)
            {
                return BadRequest("");
            }
            return Ok("Success");
        }

        #endregion

        #region Language

        [HttpGet("getLanguages")]
        public JsonResult GetLanguages()
        {
            var obj = BlAppLanguage.GetAll();
            return Json(obj);
        }

        [HttpGet("getLanguage/{id}")]
        public JsonResult GetLanguage(string id)
        {
            var obj = BlAppLanguage.GetByCode(id);

            return Json(obj);
        }

        // POST api/<controller>
        [HttpPost("postLanguage")]
        public async Task<IActionResult> PostLanguage([FromBody] DeAppLanguage obj)
        {
            try
            {
                obj = BlAppLanguage.Save(obj);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region FoodGroup

        [HttpGet("getFoodGroups")]
        public JsonResult GetFoodGroups()
        {
            var obj = BlFoodGroup.GetAll();
            return Json(obj);
        }

        [HttpGet("getFoodGroupsByLanguage/{id}")]
        public JsonResult GetFoodGroupsByLanguage(string langCode)
        {
            var obj = BlFoodGroup.GetAll().Where(x => x.LangCode == langCode);
            return Json(obj);
        }
        #endregion

        
    }
}
