using BookStore.Service.AuthorService;
using BookStore.Service.TagService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet("{id}")]
        public IActionResult GetTagById(int id)
        {
            var res = _tagService.GetTagById(id);
            return StatusCode(res.Code, res);
        }
        [HttpGet]
        public IActionResult GetTags(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var res = _tagService.GetTags(page, pageSize, key, sortBy);
            return StatusCode(res.Code, res);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTag(int id, string name)
        {
            var res = _tagService.UpdateTag(id, name);
            return StatusCode(res.Code, res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            var res = _tagService.DeleteTag(id);
            return StatusCode(res.Code, res);
        }
        [HttpPost]
        public IActionResult CreateTag(string name)
        {
            var res = _tagService.CreateTag(name);
            return StatusCode(res.Code, res);
        }
    }
}
