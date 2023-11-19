using BookStore.DTOs.User;
using BookStore.Service.AuthService;
using BookStore.Service.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var res = _userService.GetUserById(id);
            return StatusCode(res.Code, res);
        }
        //[HttpGet("{username}")]
        //public IActionResult GetUserByUsername(string username)
        //{
        //    var res = _userService.GetUserByUsername(username);
        //    return StatusCode(res.Code, res);
        //}
        [HttpGet]
        public IActionResult GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var res = _userService.GetUsers(page, pageSize, key, sortBy);
            return StatusCode(res.Code, res);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            var res = _userService.UpdateUser(id,updateUserDTO);
            return StatusCode(res.Code, res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var res = _userService.DeleteUser(id);
            return StatusCode(res.Code, res);
        }
        [HttpPost]
        public IActionResult CreateUser(CreateUserDTO createUserDTO)
        {
            var res = _userService.CreateUser(createUserDTO);
            return StatusCode(res.Code, res);
        }
    }
}
