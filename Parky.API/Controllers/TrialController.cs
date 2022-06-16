using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parky.API.Models;
using Parky.API.Models.Dtos;
using Parky.API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parky.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ParkyOpenAPISpecTrails")]
    public class TrialController : ControllerBase
    {
        private readonly ITrialRepository _trialRepository;
        private readonly IMapper _mapper;

        public TrialController(ITrialRepository trialRepository, IMapper mapper)
        {
            _trialRepository = trialRepository;
            _mapper = mapper;
        }

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrialDto>))]
        public IActionResult GetTrials()
        {
            var trials = _trialRepository.Get();
            var response=_mapper.Map<ICollection<TrialDto>>(trials).ToList();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var trial=_trialRepository.GetById(id);
            if (trial==null)
            {
                return NotFound();
            }

            var response=_mapper.Map<TrialDto>(trial);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrialDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] TrailCreateDto trialDto)
        {
            if(trialDto == null)
            {
                return BadRequest();
            }

            if (_trialRepository.IsExistByName(trialDto.Name))
            {
                ModelState.AddModelError("", "Trial Exist!");
                return StatusCode(404, ModelState);
            }

            var trial = _mapper.Map<Trial>(trialDto);
            trial.Created = DateTime.Now;

            if (_trialRepository.Create(trial))
            {
                ModelState.AddModelError("", "Somethig went wrong when saving the record in server");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new {trailId=trial.Id}, trial);
        }

        // PUT api/<TrialController>/5
        [HttpPatch("{trailId:int}",Name ="UpdateTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] TrailUpdateDto trialDto)
        {
            if (trialDto == null)
            {
                return BadRequest();
            }

            if (!_trialRepository.IsExistById(trialDto.Id))
            {
                ModelState.AddModelError("", "Trail was not exist!");
                return BadRequest(ModelState);
            }

            var nationalPark = _trialRepository.GetById(trialDto.Id);
            if (nationalPark.Name != trialDto.Name)
            {
                if (_trialRepository.IsExistByName(trialDto.Name))
                {
                    ModelState.AddModelError("", "This name is already exist");
                    return BadRequest(ModelState);
                }
            }

            nationalPark = _mapper.Map<Trial>(trialDto);

            if (!_trialRepository.Update(nationalPark))
            {
                ModelState.AddModelError("", "Somethig went wrong when updating the record in server");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        // DELETE api/<TrialController>/5
        [HttpDelete("{trailId:int}",Name ="DeleteTrail")]
        public IActionResult Delete(int id)
        {
            if (!_trialRepository.IsExistById(id))
            {
                ModelState.AddModelError("", "Trail was not exist!");
                return BadRequest(ModelState);
            }

            var trial = _trialRepository.GetById(id);

            if (!_trialRepository.Delete(trial))
            {
                ModelState.AddModelError("", "Somethig went wrong when deleting the record in server");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
