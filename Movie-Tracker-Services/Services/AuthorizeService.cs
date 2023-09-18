using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movie_Tracker_Common.CommonMethods;
using Movie_Tracker_Common.GenericResponses;
using Movie_Tracker_Services.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Tracker_Services.Services
{
    public class AuthorizeService: IAuthorizeService
    {
        IConfiguration config;
        public AuthorizeService(IConfiguration _config)
        {
            config = _config;
        }
        public JsonResult Login(string password)
        {
            if (password == "User@123")
            {
                CommonMethods common = new CommonMethods(config);
                string token = common.CreateJWt();
                return new JsonResult(new ApiResponse<string> { Data = token, Message = ResponseMessage.Success, StatusCode = ResponseCode.Success, Result = true });
            }
            else
            {
                return new JsonResult(new ApiResponse<string> { Data = null, Message = ResponseMessage.InvalidCredentials, StatusCode = ResponseCode.Unauthorized, Result = false });
            }
        }
    }
}
