using Logistic.Common.Resources;
using Logistic.Domain.DTO;
using Logistic.Domain.DTO.Seaport;
using Logistic.Domain.Services.Logistic.Interfaces;
using Logistic.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(CustomValidationFilterAttribute))]
    public class SeaportController : ControllerBase
    {
        #region Attributes
        private readonly ISeaportServices _seaportServices;
        #endregion

        #region Builder
        public SeaportController(ISeaportServices seaportServices)
        {
            _seaportServices = seaportServices;
        }
        #endregion

        #region Services
        /// <summary>
        /// Get All Seaports
        /// </summary>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllSeaports()
        {
            List<SeaportDto> list = _seaportServices.GetAllSeaports();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };


            return Ok(response);
        }

        /// <summary>
        /// Get All Seaports By Country
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpGet]
        [Route("GetAllByCountry")]
        public IActionResult GetAllSeaportsByCountry(int countryId)
        {
            List<SeaportDto> list = _seaportServices.GetAllSeaportsByCountry(countryId);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };


            return Ok(response);
        }

        /// <summary>
        /// Insert New Seaport
        /// </summary>
        /// <param name="addSeaport"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> InsertSeaport(AddSeaportDto addSeaport)
        {
            IActionResult action;
            bool result = await _seaportServices.InsertSeaport(addSeaport);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemInserted : GeneralMessages.ItemNoInserted
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }

        /// <summary>
        /// Update Seaport
        /// </summary>
        /// <param name="seaport"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateSeaport(UpdateSeaportDto seaport)
        {
            IActionResult action;
            bool result = await _seaportServices.UpdateSeaport(seaport);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemUpdated : GeneralMessages.ItemNoUpdated
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);


            return action;
        }

        /// <summary>
        /// Delete Seaport
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteSeaport(int id)
        {
            IActionResult action;
            bool result = await _seaportServices.DeleteSeaport(id);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemDeleted : GeneralMessages.ItemNoDeleted
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }


        #endregion
    }
}
