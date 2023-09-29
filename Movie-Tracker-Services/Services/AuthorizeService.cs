using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movie_Tracker.models.Data;
using Movie_Tracker.models.Models;
using Movie_Tracker.models.ViewModels;
using Movie_Tracker_Common.CommonMethods;
using Movie_Tracker_Common.GenericResponses;
using Movie_Tracker_Common.GenericResponses;
using Movie_Tracker_Services.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Tracker_Services.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        IConfiguration config;
        MovieTrackerContext movieContext;
        public AuthorizeService(IConfiguration _config, MovieTrackerContext _movieContext)
        {
            config = _config;
            movieContext = _movieContext;
        }
        public JsonResult Login(LoginVM login)
        {
            try
            {
                User user = movieContext.Users.Where(user => user.UserId == login.UserName).FirstOrDefault();

                if (user is null || login.Password != user.Password)
                {
                    return new JsonResult(new ApiResponse<string> { Data = null, Message = ResponseMessage.InvalidCredentials, StatusCode = ResponseCode.Unauthorized, Result = false });
                }

                CommonMethods common = new CommonMethods(config);
                string token = common.CreateJWt();
                return new JsonResult(new ApiResponse<string> { Data = token, Message = ResponseMessage.LoginSuccess, StatusCode = ResponseCode.Success, Result = true });

            }

            catch
            {
                return new JsonResult(new ApiResponse<string>()
                {
                    Data = null,
                    Message = ResponseMessage.Error,
                    StatusCode = ResponseCode.InternalServerError,
                    Result = false
                });
            }

        }
    }
}
