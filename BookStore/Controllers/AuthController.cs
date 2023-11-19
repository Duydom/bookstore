using BookStore.DTOs.User;
using BookStore.Service.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public ActionResult Login(string username, string password)
        {
            var res = _authService.Login(username, password);
            return StatusCode(res.Code, res);
        }
        [HttpPost("Register")]
        public ActionResult Register(RegisterUserDTO registerUserDTO)
        {
            var res = _authService.Register(registerUserDTO);
            return StatusCode(res.Code, res);
        }
    }
}
