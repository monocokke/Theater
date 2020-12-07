using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Models.ActorRole;
using System.Threading.Tasks;

namespace Theater.Controllers
{
    [Route("api/actor_roles")]
    [ApiController]
    public class ActorRolesController : ControllerBase
    {
        private readonly IBaseService<ActorRoleDTO> _service;
        private readonly ILogger<ActorRolesController> _logger;
        private readonly IMapper _mapper;

        public ActorRolesController(IBaseService<ActorRoleDTO> service, ILogger<ActorRolesController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation("Get all actors roles");
            IEnumerable<ActorRoleDTO> performances = await _service.GetAllAsync();
            if (performances == null)
                return NoContent();
            return Ok(performances);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateActorRoleModel model)
        {
            _logger.LogInformation($"Create actor role");
            if (ModelState.IsValid)
            {
                if (await _service.CreateAsync(_mapper.Map<ActorRoleDTO>(model)))
                    return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation($"Get actor role by id: {id}");
            if (id <= 0)
                return BadRequest();
            ActorRoleDTO actorRole = await _service.GetByIdAsync(id);
            if (actorRole == null)
                return NoContent();
            return Ok(actorRole);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateActorRoleModel model)
        {
            _logger.LogInformation($"Update {model.Id} actor role");
            if (ModelState.IsValid)
            {
                if (await _service.UpdateAsync(_mapper.Map<ActorRoleDTO>(model)))
                    return Ok();
                return BadRequest();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete {id} actor role");
            if (id > 0)
            {
                if (await _service.DeleteAsync(id))
                    return Ok();
                return NoContent();
            }
            return BadRequest();
        }
    }
}

