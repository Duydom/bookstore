using BookStore.Service.AuthorService;
using BookStore.Service.PublisherService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        [HttpGet("{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var res = _publisherService.GetPublisherById(id);
            return StatusCode(res.Code, res);
        }
        [HttpGet]
        public IActionResult GetPublishers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var res = _publisherService.GetPublishers(page, pageSize, key, sortBy);
            return StatusCode(res.Code, res);
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePublisher(int id, string name)
        {
            var res = _publisherService.UpdatePublisher(id, name);
            return StatusCode(res.Code, res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePublisher(int id)
        {
            var res = _publisherService.DeletePublisher(id);
            return StatusCode(res.Code, res);
        }
        [HttpPost]
        public IActionResult CreatePublisher(string name)
        {
            var res = _publisherService.CreatePublisher(name);
            return StatusCode(res.Code, res);
        }
    }
}
