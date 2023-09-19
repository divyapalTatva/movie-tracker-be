using Microsoft.AspNetCore.Mvc;
using Movie_Tracker_Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Tracker_Services.Service_Interfaces
{
    public interface IAuthorizeService
    {
        public JsonResult Login(LoginVM login);
    }
}
