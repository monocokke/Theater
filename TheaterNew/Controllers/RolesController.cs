using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Theater.Domain.Core.Models;
using Theater.Services.Interfaces;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IService<Role> _service;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IService<Role> service, ILogger<RolesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all roles");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Role role)
        {
            _logger.LogInformation($"Get role: {HttpContext.Request.Query}");
            if (_service.CreateItem(role))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get role by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete {id} role");
            if (_service.Delete(id))
                return Ok();
            return BadRequest();
        }
    }
}