using LevoWebAPI.IoC;
using LevoWebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace LevoWebAPI.Services
{
    public class UserService : IUserService, IService
    {
        private readonly UserDBContext _context;

        public UserService(UserDBContext context)
        {
            _context = context;
        }
        public IEnumerable<UserInfo> GetAllUser()
        {
            return _context.UserInfo.ToList();
        }

        public string PostUser(UserInfo userInfo)
        {
            if (UserInfoExists(userInfo.Email))
                return "User already exist";

            _context.UserInfo.Add(userInfo);
            _context.SaveChangesAsync();
            return "User Successfully Added";
        }
        private bool UserInfoExists(string email)
        {
            return _context.UserInfo.Any(x => x.Email.Equals(email));
        }
    }
}