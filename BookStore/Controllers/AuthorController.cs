using BookStore.DTOs.User;
using BookStore.Service.AuthorService;
using BookStore.Service.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var res = _authorService.GetAuthorById(id);
            return StatusCode(res.Code, res);
        }
        [HttpGet]
        public IActionResult GetAuthors(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var res = _authorService.GetAuthors(page,pageSize,key,sortBy);
            return StatusCode(res.Code, res);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, string name)
        {
            var res = _authorService.UpdateAuthor(id, name);
            return StatusCode(res.Code, res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var res = _authorService.DeleteAuthor(id);
            return StatusCode(res.Code, res);
        }
        [HttpPost]
        public IActionResult CreateAuthor(string name)
        {
            var res = _authorService.CreateAuthor(name);
            return StatusCode(res.Code, res);
        }
    }
}
