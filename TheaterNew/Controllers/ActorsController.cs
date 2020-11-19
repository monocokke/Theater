using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Models.Actor;

namespace Theater.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IService<ActorDTO> _service;
        private readonly ILogger<ActorsController> _logger;
        private readonly IMapper _mapper;

        public ActorsController(IService<ActorDTO> service, ILogger<ActorsController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all actors");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateActorModel model)
        {
            _logger.LogInformation($"Create actor");
            if (ModelState.IsValid)
            {
                if (_service.CreateItem(_mapper.Map<ActorDTO>(model)))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get actor by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateActorModel model)
        {
            _logger.LogInformation($"Update {model.Id} actor");
            if (ModelState.IsValid)
            {
                if (_service.Update(_mapper.Map<ActorDTO>(model)))
                    return Ok();
            }
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
