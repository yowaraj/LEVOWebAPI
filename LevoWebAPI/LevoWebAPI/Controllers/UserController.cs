using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LevoWebAPI.Models;
using LevoWebAPI.Helpers;
using LevoWebAPI.Services;

namespace LevoWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [CustomAuthorization, HttpGet]
        public IEnumerable<UserInfo> GetAllUser()
        {
            return _userService.GetAllUser();
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(UserInfo userInfo)
        { 
            return Ok(_userService.PostUser(userInfo));
        }
    }
}
