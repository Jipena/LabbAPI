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
    [Route("api/WebURLApi")]
    [ApiController]
    public class WebURLApiController : Controller
    {
        private readonly IRepository<WebURL> _RepoDb;
        private readonly IMapper _mapper;
        protected ApiResponse _apiResponse;
        public WebURLApiController(IRepository<WebURL> context, IMapper mapper)
        {
            _RepoDb = context;
            _mapper = mapper;
            this._apiResponse = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult <ApiResponse>> GetUrl(int userid)
        {
            try
            {
                IEnumerable<WebURL> personInterestLink = new List<WebURL>();
                if (userid > 0)
                {
                    personInterestLink = await _RepoDb.GetAllAsync(person => person.FkPersonId == userid);
                }
                _apiResponse.Result = _mapper.Map<List<WebURLDto>>(personInterestLink);
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
        public async Task<ActionResult<ApiResponse>> CreateWebUrl([FromBody] WebURLDto createDto)
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
                WebURL weblink = _mapper.Map<WebURL>(createDto);
                await _RepoDb.CreateAsync(weblink);
                _apiResponse.Result = _mapper.Map<WebURLDto>(weblink);
                _apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetUrl", new { id = weblink.WebURLId }, _apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpDelete("{id:int}", Name = "DeleteWebUrl")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteWebUrl(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var weblink = await _RepoDb.GetAsync(url => url.WebURLId == id);
                if (weblink == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                await _RepoDb.RemoveAsync(weblink);
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
