using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Models.Actor;
using System.Threading.Tasks;

namespace Theater.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IBaseService<ActorDTO> _service;
        private readonly ILogger<ActorsController> _logger;
        private readonly IMapper _mapper;

        public ActorsController(IBaseService<ActorDTO> service, ILogger<ActorsController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation("Get all actors");
            IEnumerable<ActorDTO> actors = await _service.GetAllAsync();
            if (actors == null)
                return NoContent();
            return Ok(actors);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateActorModel model)
        {
            _logger.LogInformation($"Create actor");
            if (ModelState.IsValid)
            {
                if (await _service.CreateAsync(_mapper.Map<ActorDTO>(model)))
                    return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation($"Get actor by id: {id}");
            if (id <= 0)
                return BadRequest();
            ActorDTO actor = await _service.GetByIdAsync(id);
            if (actor == null)
                return NoContent();
            return Ok(actor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateActorModel model)
        {
            _logger.LogInformation($"Update {model.Id} actor");
            if (ModelState.IsValid)
            {
                if (await _service.UpdateAsync(_mapper.Map<ActorDTO>(model)))
                    return Ok();
                return BadRequest();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete {id} actor");
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
