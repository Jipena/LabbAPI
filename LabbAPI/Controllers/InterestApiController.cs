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
    [Route("api/InterestApi")]
    [ApiController]
    public class InterestApiController : Controller
    {
        private readonly IRepository<Interest> _RepoDb;
        private readonly IMapper _mapper;
        protected ApiResponse _apiResponse;
        public InterestApiController(IRepository<Interest> context, IMapper mapper)
        {
            _RepoDb = context;
            _mapper = mapper;
            this._apiResponse = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult <ApiResponse>> GetInterest()
        {
            try
            {
                IEnumerable<Interest> interestList = await _RepoDb.GetAllAsync();
                _apiResponse.Result = _mapper.Map<List<InterestDto>>(interestList);
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

        [HttpGet("{id:int}", Name ="GetInterest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetInterest(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var interest = await _RepoDb.GetAsync(inte => inte.InterestId == id);
                if (interest == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.Result = _mapper.Map<InterestDto>(interest);
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
        public async Task<ActionResult<ApiResponse>> CreateInterest([FromBody] InterestCreateDto createDto)
        {
            try
            {
                if (await _RepoDb.GetAsync(inte => inte.Title.ToLower() == createDto.Title.ToLower()) != null)   //// för interest om det redan finns? Title
                {
                    ModelState.AddModelError("Custom Error", "This Interest Title already exist");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);  //createDto
                }
                Interest interest = _mapper.Map<Interest>(createDto);
                await _RepoDb.CreateAsync(interest);
                _apiResponse.Result = _mapper.Map<InterestDto>(interest);
                _apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetInterest", new { id = interest.InterestId }, _apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpDelete("{id:int}", Name = "DeleteInterest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteInterest(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var interest = await _RepoDb.GetAsync(inte => inte.InterestId == id);
                if (interest == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                await _RepoDb.RemoveAsync(interest);
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

        [HttpPut("{id:int}", Name = "UpdateInterest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateInterest(int id, [FromBody] InterestUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.InterestId)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                Interest model = _mapper.Map<Interest>(updateDto);
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

        [HttpPatch("{id:int}", Name = "UpdatePartialInterest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialInterest(int id, JsonPatchDocument<InterestUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var interest = await _RepoDb.GetAsync(inte => inte.InterestId == id, tracked: false);
            InterestUpdateDto interestDto = _mapper.Map<InterestUpdateDto>(interest);
            if (interest == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(interestDto, ModelState);
            Interest model = _mapper.Map<Interest>(interestDto);
            await _RepoDb.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
