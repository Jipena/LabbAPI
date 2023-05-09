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
    [Route("api/PersonInterestApi")]
    [ApiController]
    public class PersonInterestApiController : Controller
    {
        private readonly IRepository<PersonInterest> _RepoDb;
        private readonly IMapper _mapper;
        protected ApiResponse _apiResponse;
        public PersonInterestApiController(IRepository<PersonInterest> context, IMapper mapper)
        {
            _RepoDb = context;
            _mapper = mapper;
            this._apiResponse = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetPersonInterest(int userid=0)
        {
            try
            {
                IEnumerable<PersonInterest> personInterestList = new List<PersonInterest>();
                if (userid >0)
                {
                    personInterestList = await _RepoDb.GetAllAsync(person => person.FkPersonId == userid);
                }
                else 
                {
                    personInterestList = await _RepoDb.GetAllAsync();
                }
                _apiResponse.Result = _mapper.Map<List<PersonInterestDto>>(personInterestList);
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
        public async Task<ActionResult<ApiResponse>> CreatePersonInterest([FromBody] PersonInterestDto createDto)
        {
            try
            {
                //if (await _RepoDb.GetAsync(pein => pein.PersonInterestId.ToLower() == createDto..ToLower()) != null)   //// för interest om det redan finns? Title
                //{
                //    ModelState.AddModelError("Custom Error", "This person already exist");
                //    return BadRequest(ModelState);
                //}
                if (createDto == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);  //createDto
                }

                if (await _RepoDb.GetAsync(pedb => pedb.Person.PersonId == createDto.FkPersonId) == null)
                {
                    ModelState.AddModelError("Custom Error", "Person id is not valid");
                    return BadRequest(ModelState);
                }
                if (await _RepoDb.GetAsync(indb => indb.Interest.InterestId == createDto.FkInterestId) == null)
                {
                    ModelState.AddModelError("Custom Error", "Interest id is not valid");
                    return BadRequest(ModelState);
                }

                PersonInterest personInterest = _mapper.Map<PersonInterest>(createDto);
                await _RepoDb.CreateAsync(personInterest);
                _apiResponse.Result = _mapper.Map<PersonInterestDto>(personInterest);
                _apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetAll", new { id = personInterest.PersonInterestId }, _apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpDelete("{id:int}", Name = "DeletePersonInterest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeletePersonInterest(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var personInterest = await _RepoDb.GetAsync(pe => pe.PersonInterestId == id);
                if (personInterest == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                await _RepoDb.RemoveAsync(personInterest);
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
    }
}
