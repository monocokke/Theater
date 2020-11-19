using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Models.Role;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IService<RoleDTO> _service;
        private readonly ILogger<RolesController> _logger;
        private readonly IMapper _mapper;

        public RolesController(IService<RoleDTO> service, ILogger<RolesController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all roles");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateRoleModel model)
        {
            _logger.LogInformation($"Create role");
            if (ModelState.IsValid)
            {
                if (_service.CreateItem(_mapper.Map<RoleDTO>(model)))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get role by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateRoleModel model)
        {
            _logger.LogInformation($"Update {model.Id} role");
            if (ModelState.IsValid)
            {
                if (_service.Update(_mapper.Map<RoleDTO>(model)))
                    return Ok();
            }
            return BadRequest();
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