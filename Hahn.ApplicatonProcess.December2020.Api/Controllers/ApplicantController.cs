using Hahn.ApplicatonProcess.December2020.Api.Utilities.Validators;
using Hahn.ApplicatonProcess.December2020.Data.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly ILogger _logger;

        public ApplicantController(IApplicantService applicantService, ILogger<ApplicantController> logger)
        {
            this._applicantService = applicantService;
            this._logger = logger;
        }

        /// <summary>
        /// Creates a new  Applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/CreateApplicant
        ///     {        
        ///       "Name": "Matthias",
        ///       "FamilyName": "Plottke",
        ///       "Address": "Pürschläger Weg 5" ,
        ///       "CountryOfOrigin":"Deutschland",
        ///       "EmailAddress":"Matthias.Plottke@WiBiZu.de",
        ///       "Age":38,
        ///       "Hired":false
        ///     }
        /// </remarks>
        /// <param name="applicant"></param>    
        /// <returns>A url of newly created applicant</returns>
        /// <response code="201">Returns a url of newly created applicant</response>
        /// <response code="400">Returns, errors,if the applicant object is invalid</response>     
        [HttpPost("CreateApplicant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateApplicant([FromBody] Applicant applicant)
        {
            try
            {
                if (applicant.IsValid(out IEnumerable<string> errors))
                {
                    var result = await _applicantService.Create(applicant);

                    _logger.LogInformation("Succesfully created applicant @{object}, at @{url}", applicant, "api/Applicant/" + result.Id);

                    return CreatedAtAction(nameof(GetApplicantById),
                        new { id = result.Id }, "api/Applicant/" + result.Id);

                }
                else
                {
                    _logger.LogError("Invalid params at CreateApplicant Errors: {@errors}, Object:{@object}", errors, applicant);
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at CreateApplicant {@exception}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
               
            }
        }
        /// <summary>
        ///Updates an existing applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/UpdateApplicant
        ///     {  id:"1",      
        ///       "Name": "Matthias",
        ///       "FamilyName": "Plottke",
        ///       "Address": "Pürschläger Weg 5" ,
        ///       "CountryOfOrigin":"Deutschland",
        ///       "EmailAddress":"Matthias.Plottke@WiBiZu.de",
        ///       "Age":38,
        ///       "Hired":false
        ///     }
        /// </remarks>
        /// <param name="applicant"></param>    
        /// <response code="201">Returns the object of updated applicant</response>
        /// <response code="400">Returns errors, if the applicant object is invalid</response> 
        [HttpPut("UpdateApplicant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateApplicant([FromBody] Applicant applicant)
        {
            try
            {
                if (applicant.IsValid(out IEnumerable<string> errors))
                {
                    var result = await _applicantService.Update(applicant);

                    return CreatedAtAction(nameof(GetApplicantById),
                        new { id = result.Id }, "api/Applicant/" + result.Id);
                }
                else
                {
                    _logger.LogError("Invalid params at UpdateApplicant Errors: {@errors}, Applicant:{@object} ", errors, applicant);
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at UpdateApplicant {@Exception} ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,"Something went wrong");
            }
        }

        /// <summary>
        ///Retrieves all applicants.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET api/GetAllApplicants
        /// </remarks>  
        /// <response code="200">Returns the objects of existing applicants</response>
        /// <response code="200">Returns No content if no existing applicants </response>
        [HttpGet("GetAllApplicants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllApplicants()
        {
            try
            {
                var result = _applicantService.GetAll();

                if (result.Any())
                    return Ok("No Content");

                _logger.LogInformation("Successfully retrieved all applicants {@object}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at GetAllApplicants {@exception} ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError,"Something went wrong");
            }
        }

        /// <summary>
        ///Retrieves single applicant by Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/GetApplicantById/1
        /// </remarks>
        /// <param name="id"></param>    
        /// <response code="200">Returns the object existing applicant by Id</response>
        /// <response code="204">Returns No Content </response>
        /// <response code="400">Returns Invalid parameter </response>
        [HttpGet("GetApplicantById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Applicant> GetApplicantById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = _applicantService.Get(id);
                    if (result == null)
                        return Ok("No Content");

                    _logger.LogInformation("Successfully retrieved applicant by Id {@object}", result);
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("BadRequest at GetApplicantById @{id}", id);
                    return BadRequest("Invalid parameters");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at GetApplicantById @{exception} ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        /// <summary>
        ///Delete single applicant by Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/DeleteApplicantById/1
        /// </remarks>
        /// <param name="id"></param>    
        /// <response code="200">Returns Success if deletion successful</response>
        /// <response code="204">Returns No Content if no matching applicant </response>
        /// <response code="400">Returns Invalid parameter </response>
        [HttpDelete("DeleteApplicantById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteApplicantById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _applicantService.Delete(id);

                    if (!result)
                        return Ok("No Content");

                    _logger.LogInformation("Successfully deleted applicant by Id {@Id}", id);
                    return Ok("Success");
                }
                else
                {
                    _logger.LogError("BadRequest at DeleteApplicantById {@Id} ", id);
                    return BadRequest("Invalid parameter");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at DeleteApplicantById {@exception} ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        /// <summary>
        ///Search applicants by Name and Family name.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/SearchApplicants
        ///     "Matthias"
        ///
        /// </remarks>  
        /// <response code="200">Returns Success if match found</response>
        /// <response code="204">Returns No Content if no matching applicant </response>
        /// <response code="400">Returns Invalid parameter </response>
        [HttpPost("SearchApplicants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Applicant> SearchApplicants([FromBody] string keyword)
        {
            try
            {
                if (keyword!=null)
                {
                    var result = _applicantService.Search(keyword);

                    if (result==null)
                        return Ok("No Content");

                    _logger.LogInformation("Successfully fetched applicant {@keyword}", keyword);
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("BadRequest at SearchApplicants {@keyword} ", keyword);
                    return BadRequest("Invalid parameter");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at SearchApplicants {@exception} ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

    }
}
