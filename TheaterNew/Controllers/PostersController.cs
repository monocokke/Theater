using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Models.Poster;
using System.Threading.Tasks;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostersController : ControllerBase
    {
        private readonly IBaseService<PosterDTO> _service;
        private readonly ILogger<PostersController> _logger;
        private readonly IMapper _mapper;

        public PostersController(IBaseService<PosterDTO> service, ILogger<PostersController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation("Get all posters");
            IEnumerable<PosterDTO> performances = await _service.GetAllAsync();
            if (performances == null)
                return NoContent();
            return Ok(performances);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreatePosterModel model)
        {
            _logger.LogInformation($"Create poster");
            if (ModelState.IsValid)
            {
                if (await _service.CreateAsync(_mapper.Map<PosterDTO>(model)))
                    return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation($"Get poster by id: {id}");
            if (id <= 0)
                return BadRequest();
            PosterDTO poster = await _service.GetByIdAsync(id);
            if (poster == null)
                return NoContent();
            return Ok(poster);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePosterModel model)
        {
            _logger.LogInformation($"Update {model.Id} poster");
            if (ModelState.IsValid)
            {
                if (await _service.UpdateAsync(_mapper.Map<PosterDTO>(model)))
                    return Ok();
                return BadRequest();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Delete {id} poster");
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
