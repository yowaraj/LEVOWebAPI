using LevoWebAPI.Models;
using System.Collections.Generic;

namespace LevoWebAPI.Services
{
    public interface IUserService
    {
        IEnumerable<UserInfo> GetAllUser();
        string PostUser(UserInfo userInfo);
    }
}