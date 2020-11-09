using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Theater.Domain.Core.Models;
using Theater.Services.Interfaces;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly IService<Performance> _service;
        private readonly ILogger<PerformancesController> _logger;

        public PerformancesController(IService<Performance> service, ILogger<PerformancesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all performances");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Performance performance)
        {
            _logger.LogInformation($"Create performance");
            if (_service.CreateItem(performance))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get performance by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Performance performance)
        {
            _logger.LogInformation($"Update {performance.Id} performance");
            if(_service.Update(performance))
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete {id} performance");
            if (_service.Delete(id))
                return Ok();
            return BadRequest();
        }
    }
}
