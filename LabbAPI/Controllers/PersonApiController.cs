using AutoMapper;
using LabbAPI.Data;
using LabbAPI.Models;
using LabbAPI.Models.DTO;
using LabbAPI.Repository.Irepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LabbAPI.Controllers
{
    [Route("api/PersonApi")]
    [ApiController]
    public class PersonApiController : Controller
    {
        private readonly IRepository<Person> _RepoDb;
        private readonly IMapper _mapper;
        protected ApiResponse _apiResponse;
        public PersonApiController(IRepository<Person> context, IMapper mapper)
        {
            _RepoDb = context;
            _mapper = mapper;
            this._apiResponse = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult <ApiResponse>> GetPerson()
        {
            try
            {
                IEnumerable<Person> personList = await _RepoDb.GetAllAsync();
                _apiResponse.Result = _mapper.Map<List<PersonDto>>(personList);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpGet("{id:int}", Name ="GetPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetPerson(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var person = await _RepoDb.GetAsync(pe => pe.PersonId == id);
                if (person == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.Result = _mapper.Map<PersonDto>(person);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _apiResponse;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreatePerson([FromBody] PersonCreateDto createDto)
        {
            try
            {
                //if (await _RepoDb.GetAsync(pe=>pe.FirstName.ToLower() == createDto.FirstName.ToLower())!=null)   //// för interest om det redan finns? Title
                //{
                //    ModelState.AddModelError("Custom Error", "This person already exist");
                //    return BadRequest(ModelState);
                //}
                if (createDto == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);  //createDto
                }
                Person person = _mapper.Map<Person>(createDto);
                await _RepoDb.CreateAsync(person);
                _apiResponse.Result = _mapper.Map<PersonDto>(person);
                _apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPerson", new { id = person.PersonId }, _apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpDelete("{id:int}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeletePerson(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var person = await _RepoDb.GetAsync(pe => pe.PersonId == id);
                if (person == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                await _RepoDb.RemoveAsync(person);
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPut("{id:int}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdatePerson(int id, [FromBody] PersonUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.PersonId)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                Person model = _mapper.Map<Person>(updateDto);
                await _RepoDb.UpdateAsync(model);
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialPerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialPerson(int id, JsonPatchDocument<PersonUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var person = await _RepoDb.GetAsync(pe => pe.PersonId == id, tracked: false);
            PersonUpdateDto personDto = _mapper.Map<PersonUpdateDto>(person);
            if (person == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(personDto, ModelState);
            Person model = _mapper.Map<Person>(personDto);
            await _RepoDb.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
