using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Theater.Domain.Core.Models;
using Theater.Services.Interfaces;

namespace Theater.Controllers
{
    [Route("api/actor_roles")]
    [ApiController]
    public class ActorRolesController : ControllerBase
    {
        private readonly IService<ActorRole> _service;
        private readonly ILogger<ActorRolesController> _logger;

        public ActorRolesController(IService<ActorRole> service, ILogger<ActorRolesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all actors and their roles");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] ActorRole actorRole)
        {
            _logger.LogInformation($"Get actor's role: {actorRole.Id}");
            if (_service.CreateItem(actorRole))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get actor's role by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] ActorRole actorRole)
        {
            _logger.LogInformation($"Update {actorRole.Id} actor's role");
            if (_service.Update(actorRole))
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete {id} actor's role");
            if (_service.Delete(id))
                return Ok();
            return BadRequest();
        }
    }
}
