using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Tracker_Services.Service_Interfaces;

namespace Movie_Tracker_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthorizeService authService;
        public AuthenticationController(IAuthorizeService _authService)
        {
            authService = _authService;
        }

        [HttpPost("Login")]
        public JsonResult Login(string password)
        {
            return authService.Login(password);
        }
    }
}
