using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Theater.Domain.Core.Models;
using Theater.Services.Interfaces;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostersController : ControllerBase
    {
        private readonly IService<Poster> _service;
        private readonly ILogger<PostersController> _logger;

        public PostersController(IService<Poster> service, ILogger<PostersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all posters");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Poster poster)
        {
            _logger.LogInformation($"Get poster: {HttpContext.Request.Query}");
            if (_service.CreateItem(poster))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get poster by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Poster poster)
        {
            _logger.LogInformation($"Update {poster.Id} poster");
            if (_service.Update(poster))
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete {id} poster");
            if (_service.Delete(id))
                return Ok();
            return BadRequest();
        }
    }
}

