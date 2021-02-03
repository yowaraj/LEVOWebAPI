using LevoWebAPI.Models;
using LevoWebAPI.Services;
using System.Collections.Generic;
using System.Linq;


namespace NUnitTestLevoWebAPI.Services
{
    public class UserServiceFake : IUserService
    {
        private readonly List<UserInfo> _userInfo;
        public UserServiceFake()
        {
            _userInfo = new List<UserInfo>()
            {
                new UserInfo(){Id=1, FirstName="Yowaraj", LastName="Chhetri", Email="uo@gm.c"},
                new UserInfo(){Id=2, FirstName="Yowaraj", LastName="Chhetri", Email="uo@gm.c1"},
            };              
        }
        public IEnumerable<UserInfo> GetAllUser()
        {
            return _userInfo;
        }

        public string PostUser(UserInfo userInfo)
        {
            if (UserInfoExists(userInfo.Email))
                return "User already exist";

            _userInfo.Add(userInfo);
            return "User Successfully Added";
        }
        private bool UserInfoExists(string email)
        {
            return _userInfo.Any(x => x.Email.Equals(email));
        }
    }
}