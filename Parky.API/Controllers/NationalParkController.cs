using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parky.API.Models;
using Parky.API.Models.Dtos;
using Parky.API.Repository.IRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parky.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public NationalParkController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var nationalparks = _nationalParkRepository.Get();

            var nationalParksDto = _mapper.Map<ICollection<NationalParkDto>>(nationalparks);

            return Ok(nationalParksDto);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var nationalPark = _nationalParkRepository.GetById(id);
            if (nationalPark == null)
            {
                return NotFound();
            }

            var nationalParkDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(nationalParkDto);
        }

        [HttpPost]
        [ProducesResponseType(201,Type =typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Create([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest();
            }

            if (_nationalParkRepository.IsExistByName(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "NationalPark Exisst!");
                return StatusCode(404, ModelState);
            }

            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            nationalPark.Created = DateTime.Now;

            if (!_nationalParkRepository.Create(nationalPark))
            {
                ModelState.AddModelError("", "Somethig went wrong when saving the record in server");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpPatch]
        public IActionResult Update([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest();
            }

            if (!_nationalParkRepository.IsExistById(nationalParkDto.Id))
            {
                ModelState.AddModelError("", "National Park was not exist!");
                return BadRequest(ModelState);
            }

            var nationalPark = _nationalParkRepository.GetById(nationalParkDto.Id);
            if (nationalPark.Name != nationalParkDto.Name)
            {
                if (_nationalParkRepository.IsExistByName(nationalParkDto.Name))
                {
                    ModelState.AddModelError("", "This name is already exist");
                    return BadRequest(ModelState);
                }
            }

            nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_nationalParkRepository.Update(nationalPark))
            {
                ModelState.AddModelError("", "Somethig went wrong when updating the record in server");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!_nationalParkRepository.IsExistById(id))
            {
                ModelState.AddModelError("", "National Park was not exist!");
                return BadRequest(ModelState);
            }

            var nationalPark=_nationalParkRepository.GetById(id);

            if (!_nationalParkRepository.Delete(nationalPark))
            {
                ModelState.AddModelError("", "Somethig went wrong when deleting the record in server");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }

}
