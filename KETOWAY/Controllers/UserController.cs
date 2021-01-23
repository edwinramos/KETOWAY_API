using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KetoWay.DataAccess.DataEntities;
using KETOWAY.DataAccess;
using KETOWAY.Helpers;
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
    public class UserController : Controller
    {
        #region LogIn
        [HttpPost("userLogIn")]
        public ApiResponse UserLogIn([FromBody] DeUser user)
        {
            var result = BlUser.IsValidUser(user.UserCode, user.Password);

            //if (result.Success)
            //{
            //    CookieHelper.CreateCookie(Request, Response, "activeUser", user.UserCode, DateTime.Now.AddMinutes(3));
            //    //string activeUser = Request.Cookies["activeUser"];

            //    //if (string.IsNullOrEmpty(activeUser))
            //    //{
            //    //    CookieOptions option = new CookieOptions();

            //    //    option.Expires = 

            //    //    Response.Cookies.Append("activeUser", ((DeUser)result.Body).UserCode, option);
            //    //}
            //}
            return result;
        }
        #endregion

        #region User
        [HttpGet]
        [Route("getUsers")]
        public JsonResult GetAllUsers()
        {
            var result = BlUser.GetAll();

            return Json(result);
        }

        [Route("getUser/{userCode}")]
        public JsonResult GetUser(string userCode)
        {
            var obj = BlUser.GetAll().FirstOrDefault(x => x.UserCode == userCode);
            if (obj == null)
                obj = new DeUser
                {
                    UserCode = "",
                    Name = "",
                    LastName = "",
                    Email = "",
                    Password = "",
                    ImagePath = "",
                    BirthDate = DateTime.Today
                };

            return Json(obj);
        }

        [HttpPost("postUser")]
        public async Task<IActionResult> PostUser([FromBody] object model)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<DeUser>(model.ToString());
                BlUser.Save(obj);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] string userCode)
        {
            try
            {
                BlUser.Delete(userCode);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("");
            }
        }

        [HttpPost("userImage")]
        public async Task<IActionResult> UserImage(IFormFile model)
        {
            try
            {
                //if (formFile.Length > 0)
                //{
                //    var filePath = @"~/UserImages";//Path.GetTempFileName();

                //    using (var stream = System.IO.File.Create(filePath))
                //    {
                //        await formFile.CopyToAsync(stream);
                //    }
                //}

                // Process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("");
            }
        }
        #endregion
    }
}
