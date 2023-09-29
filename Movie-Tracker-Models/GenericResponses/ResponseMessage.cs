using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Tracker_Common.GenericResponses
{
    public class ResponseMessage
    {
        public static string Success = "Success";
        public static string Error = "Some Error Occured";
        public static string MovieNotFound = "Movie Does Not exists";
        public static string InvalidCredentials= "User Id or Password is incorrect";
        public static string LoginSuccess= "Login Successfull";

    }
}
