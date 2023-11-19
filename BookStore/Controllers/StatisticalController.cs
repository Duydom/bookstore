using BookStore.Service.StatisticalService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService _statisticalService;
        public StatisticalController(IStatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }
        [HttpGet]
        public IActionResult GetStatistical()
        {
            var res = _statisticalService.GetStatistical();
            return StatusCode(res.Code, res);
        }
    }
}
