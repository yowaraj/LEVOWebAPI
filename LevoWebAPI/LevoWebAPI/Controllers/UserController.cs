using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LevoWebAPI.Models;
using LevoWebAPI.Helpers;

namespace LevoWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDBContext _context;
        public UserController(UserDBContext context)
        {
            _context = context;
        }

        [CustomAuthorization, HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllUser()
        {
            return await _context.UserInfo.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(UserInfo userInfo)
        {           
            if(UserInfoExists(userInfo.Email))
                return BadRequest(new { message = "User already exist" });

            _context.UserInfo.Add(userInfo);
            await _context.SaveChangesAsync();

            return Ok("Successfully Added");
        }

        private bool UserInfoExists(string email)
        {
            return _context.UserInfo.Any(x => x.Email.Equals(email));
        }
    }
}
