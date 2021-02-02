using LevoWebAPI.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace LevoWebAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {

        /// <summary>  
        /// This will Authorize User  
        /// </summary>  
        /// <returns></returns>  
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var _db = filterContext.HttpContext.RequestServices.GetService(typeof(UserDBContext)) as UserDBContext;

            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues authTokens;
                filterContext.HttpContext.Request.Headers.TryGetValue("authToken", out authTokens);

                var _token = authTokens.FirstOrDefault();

                if (_token != null)
                {
                    if (IsValidToken(_token, _db))
                    {
                        filterContext.HttpContext.Response.Headers.Add("authToken", _token);
                        filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                        filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                        return;
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Headers.Add("authToken", _token);
                        filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                        filterContext.Result = new JsonResult("NotAuthorized")
                        {
                            Value = new
                            {
                                Status = "Error",
                                Message = "Please provide a valid authorisation token to access this endpoint."
                            },
                        };
                    }                    

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authToken";
                    filterContext.Result = new JsonResult("Please Provide authToken")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please Provide authToken"
                        },
                    };
                }
            }
        }

        public bool IsValidToken(string authToken, UserDBContext _db)
        {
            string decodedAuthenticationToken = Encoding.UTF8.GetString(
            Convert.FromBase64String(authToken));
            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string email = usernamePasswordArray[0];

            var UserLists = _db.UserInfo;

            return UserLists.Any(user =>
                user.Email.Equals(email));
        }
    }
}