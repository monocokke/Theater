using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Models.ActorRole;

namespace Theater.Controllers
{
    [Route("api/actor_roles")]
    [ApiController]
    public class ActorRolesController : ControllerBase
    {
        private readonly IService<ActorRoleDTO> _service;
        private readonly ILogger<ActorRolesController> _logger;
        private readonly IMapper _mapper;

        public ActorRolesController(IService<ActorRoleDTO> service, ILogger<ActorRolesController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all actors and their roles");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateActorRoleModel model)
        {
            _logger.LogInformation($"Create actor's role");
            if (ModelState.IsValid)
            {
                if (_service.CreateItem(_mapper.Map<ActorRoleDTO>(model)))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get actor's role by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateActorRoleModel model)
        {
            _logger.LogInformation($"Update {model.Id} actor's role");
            if (ModelState.IsValid)
            {
                if (_service.Update(_mapper.Map<ActorRoleDTO>(model)))
                    return Ok();
            }
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
