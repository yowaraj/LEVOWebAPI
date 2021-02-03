using LevoWebAPI.Controllers;
using LevoWebAPI.Helpers;
using LevoWebAPI.Models;
using LevoWebAPI.Services;
using NUnit.Framework;
using NUnitTestLevoWebAPI.Services;
using System.Linq;

namespace NUnitTestLevoWebAPI.Controllers
{
    public class UserControllerTest
    {
        UserController _userController;
        IUserService _userService;

        public UserControllerTest()
        {
            _userService = new UserServiceFake();
            _userController = new UserController(_userService);
        }

        [Test]
        public void Get_WhenCalled_ReturnsAllUsers()
        {
            var result = _userController.GetAllUser();
            var customAttribute = _userController.GetType().GetMethod("GetAllUser").GetCustomAttributes(typeof(CustomAuthorization), true);
            
            // Assert
            Assert.AreEqual(2, result.Count());           
            Assert.AreEqual(typeof(CustomAuthorization), customAttribute[0].GetType());
        }

        [Test]
        public void Add_ExistingUser_ReturnsAlreadyExistString()
        {
            // Arrange
            var existingUser = new UserInfo()
            {
                Id=1,
                FirstName="Yowaraj",
                LastName="Chhetri",
                Email= "uo@gm.c"
            };

            // Act
            var badResponse = _userController.PostUser(existingUser);
            
            // Assert
            Assert.IsNotNull(badResponse.Result);
        }

        [Test]
        public void Add_AddUser_ReturnsOk()
        {
            // Arrange
            var newUser = new UserInfo()
            {
                Id = 1,
                FirstName = "Yowaraj",
                LastName = "Chhetri",
                Email = "uo@gm.c123"
            };

            // Act
            var response = _userController.PostUser(newUser);

            // Assert
            Assert.IsNotNull(response.Result);
        }
    }
}
