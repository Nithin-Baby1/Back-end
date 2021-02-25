using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webAPI2.Models;

using webAPI2.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile(string id = "")
        {
            string userId = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                userId = User.Claims.First(c => c.Type == "UserID").Value;
            }
            else
            {
                userId = id;
            }

            var user = await _userManager.FindByIdAsync(userId);
           
            return new
            {
                user.FullName,
                user.Email,
                user.UserName,
                user.Stream,
                user.RollNo,
                userId
            };

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Route("ForAdmin")]
        public IList<ApplicationUserModel> GetForAdmin()
        {
            IList<ApplicationUserModel> users = new List<ApplicationUserModel>();
            ///return "Web method for Admin";
            foreach(var user in _userManager.Users)
            {

                string UserId = _userManager.GetUserIdAsync(user).Result;
                users.Add(
                    new ApplicationUserModel
                    {
                       UserName = user.UserName,
                       FullName = user.FullName,
                       Email = user.Email,
                       Stream = user.Stream,
                       RollNo = user.RollNo,
                       UserId = UserId

                    }
                    
                    );
            }
            return users;
        }

        [HttpPost]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("EditUser")]
        //GET : /api/UserProfile
        public async Task<Object> EditUserProfile(ApplicationUserModel model, string id = "")
        {
            string userId = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                userId = User.Claims.First(c => c.Type == "UserID").Value;
            }
            else
            {
                userId = id;
            }

            var user = await _userManager.FindByIdAsync(userId);
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.Stream = model.Stream;
            user.RollNo = model.RollNo;

            var result = await _userManager.UpdateAsync(user);
            return result;

        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
        [Route("ForCustomer")]
        public string GetCustomer()
        {
            return "Web method for Customer";
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
        [Route("ForAdminOrCustomer")]
        public string GetForAdminOrCustomer()
        {
            return "Web method for Admin or Customer";
        }
        [HttpPost]
        [Route("Deleteuser")]
        //GET : /api/UserProfile
        public async Task<Object> Deleteuser (string id = "")
        {
            string userId = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                userId = User.Claims.First(c => c.Type == "UserID").Value;
            }
            else
            {
                userId = id;
            }

            var user = await _userManager.FindByIdAsync(userId);
            

            var result = await _userManager.DeleteAsync(user);
            return result;

        }
    }
}
