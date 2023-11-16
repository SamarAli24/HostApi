//using HostProject.Model;
using Services;
using Services.Model;
using Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Models;

namespace HostProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region fields
        private readonly hosteduContext _aplicationdbContext;
        private readonly IUserService _userService;
        HashingHelper hashingHelper = new HashingHelper();
        #endregion

        #region ctor
        public UserController(IUserService userService, hosteduContext aplicationdbContext)
        {
            this._aplicationdbContext = aplicationdbContext;
            this._userService = userService;
        }
        #endregion

        [Route("user/list")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var getToken = Request.Headers.Authorization.ToString();
                
                var list = await _userService.GetUsers();
                {
                    return new JsonResult(list);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        [Route("user/Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCUModel userModel)
        {
           
            UserSetModel setModel = new UserSetModel();
            setModel.Sex = userModel.Sex;
            setModel.Mobile = userModel.MobileNumber;
            setModel.Dob = userModel.DateOfBirth;
            setModel.JoinDate = userModel.JoinDate;
            setModel.PassHash = userModel.Password;
            setModel.Email = userModel.Email;
            setModel.FirstName = userModel.FirstName;
            setModel.LastName = userModel.FamilyName;
            setModel.Id = 0;
           
            setModel.Name = userModel.UserName;
            
            setModel.PassSalt = userModel.confirmPassword;
           
            setModel.Timestamp = DateTime.Now;
            var list = await _userService.InsertUser(setModel);
            if (list.Contains("User Successfully"))
            {
               
            }
            else
            {
                return BadRequest(list);
            }
            return new JsonResult(list);
        }
    }
}
