using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Models.Poster;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostersController : ControllerBase
    {
        private readonly IService<PosterDTO> _service;
        private readonly ILogger<PostersController> _logger;
        private readonly IMapper _mapper;

        public PostersController(IService<PosterDTO> service, ILogger<PostersController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get all posters");
            return Ok(_service.GetItems());
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePosterModel model)
        {
            _logger.LogInformation($"Get poster: {HttpContext.Request.Query}");
            if (ModelState.IsValid)
            {
                if (_service.CreateItem(_mapper.Map<PosterDTO>(model)))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get poster by id: {id}");
            return Ok(_service.GetItem(id));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdatePosterModel model)
        {
            _logger.LogInformation($"Update {model.Id} poster");
            if (ModelState.IsValid)
            {
                if (_service.Update(_mapper.Map<PosterDTO>(model)))
                    return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete {id} poster");
            if (_service.Delete(id))
                return Ok();
            return BadRequest();
        }
    }
}

