using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Models.Role;
using System.Threading.Tasks;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IBaseService<RoleDTO> _service;
        private readonly ILogger<RolesController> _logger;
        private readonly IMapper _mapper;

        public RolesController(IBaseService<RoleDTO> service, ILogger<RolesController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation("Get all roles");
            IEnumerable<RoleDTO> performances = await _service.GetAllAsync();
            if (performances == null)
                return NoContent();
            return Ok(performances);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateRoleModel model)
        {
            _logger.LogInformation($"Create role");
            if (ModelState.IsValid)
            {
                if (await _service.CreateAsync(_mapper.Map<RoleDTO>(model)))
                    return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation($"Get role by id: {id}");
            if (id <= 0)
                return BadRequest();
            RoleDTO performance = await _service.GetByIdAsync(id);
            if (performance == null)
                return NoContent();
            return Ok(performance);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleModel model)
        {
            _logger.LogInformation($"Update {model.Id} role");
            if (ModelState.IsValid)
            {
                if (await _service.UpdateAsync(_mapper.Map<RoleDTO>(model)))
                    return Ok();
                return BadRequest();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete {id} role");
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
