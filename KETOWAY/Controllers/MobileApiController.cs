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
                foodList = BlFood.GetAllFood().Where(x => x.IsAllowed && x.LangCode == langCode),
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
