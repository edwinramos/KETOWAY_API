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
    public class UserController : Controller
    {
        #region LogIn
        [HttpPost("userLogIn")]
        public ApiResponse UserLogIn([FromBody] ApiRequest model)
        {
            var user = JsonConvert.DeserializeObject<DeUser>(model.Body.ToString());
            var result = BlUser.IsValidUser(user.UserCode, user.Password);
            return result;
        }
        #endregion

        #region User
        [HttpGet]
        public ApiResponse GetAllUsers()
        {
            var result = BlUser.GetAll();
            return result;
        }

        [Route("getUser/{userCode}")]
        public ApiResponse GetUser(string userCode)
        {
            var obj = BlUser.GetByCode(userCode);
            return obj;
        }

        [HttpPost("postUser")]
        public async Task<ApiResponse> PostUser([FromBody] ApiRequest model)
        {
            var obj = JsonConvert.DeserializeObject<DeUser>(model.Body.ToString());
            var result = BlUser.Save(obj);

            return result;
        }

        [HttpDelete("{userCode}")]
        public ApiResponse DeleteUser(string userCode)
        {
            var result = BlUser.Delete(userCode);
            return result;
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
