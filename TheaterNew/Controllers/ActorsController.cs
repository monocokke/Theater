using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Theater.Domain.Core.Models;
using Theater.Services.Interfaces;

namespace Theater.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IService<Actor> _service;
        private readonly ILogger<ActorsController> _logger;

        public ActorsController(IService<Actor> service, ILogger<ActorsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all actors");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Actor actor)
        {
            _logger.LogInformation($"Get actor: {HttpContext.Request.Query}");
            if (_service.CreateItem(actor))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get actor by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Actor actor)
        {
            _logger.LogInformation($"Update {actor.Id} actor");
            if (_service.Update(actor))
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete {id} actor");
            if (_service.Delete(id))
                return Ok();
            return BadRequest();
        }
    }
}
