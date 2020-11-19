using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Models.Performance;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly IService<PerformanceDTO> _service;
        private readonly ILogger<PerformancesController> _logger;
        private readonly IMapper _mapper;

        public PerformancesController(IService<PerformanceDTO> service, ILogger<PerformancesController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all performances");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePerformanceModel model)
        {
            _logger.LogInformation($"Create performance");
            if (ModelState.IsValid)
            {
                if (_service.CreateItem(_mapper.Map<PerformanceDTO>(model)))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get performance by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdatePerformanceModel model)
        {
            _logger.LogInformation($"Update {model.Id} performance");
            if (ModelState.IsValid)
            {
                if (_service.Update(_mapper.Map<PerformanceDTO>(model)))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete {id} performance");
            if (_service.Delete(id))
                return Ok();
            return BadRequest();
        }
    }
}
