using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Models.Performance;
using System.Threading.Tasks;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly IBaseService<PerformanceDTO> _service;
        private readonly ILogger<PerformancesController> _logger;
        private readonly IMapper _mapper;

        public PerformancesController(IBaseService<PerformanceDTO> service, ILogger<PerformancesController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation("Get all performances");
            IEnumerable<PerformanceDTO> performances = await _service.GetAllAsync();
            if (performances == null)
                return NoContent();
            return Ok(performances);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreatePerformanceModel model)
        {
            _logger.LogInformation($"Create performance");
            if (ModelState.IsValid)
            {
                if (await _service.CreateAsync(_mapper.Map<PerformanceDTO>(model)))
                    return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation($"Get performance by id: {id}");
            if (id <= 0)
                return BadRequest();
            PerformanceDTO performance = await _service.GetByIdAsync(id);
            if (performance == null)
                return NoContent();
            return Ok(performance);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePerformanceModel model)
        {
            _logger.LogInformation($"Update {model.Id} performance");
            if (ModelState.IsValid)
            {
                if (await _service.UpdateAsync(_mapper.Map<PerformanceDTO>(model)))
                    return Ok();
                return BadRequest();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete {id} performance");
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
