using Logistic.Common.Resources;
using Logistic.Domain.DTO;
using Logistic.Domain.DTO.Logistic.LandLot;
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
    public class LandLotController : ControllerBase
    {
        #region Attributes
        private readonly ILandLotServices _landLotServices;
        #endregion

        #region Builder
        public LandLotController(ILandLotServices landLotServices)
        {
            _landLotServices = landLotServices;
        }
        #endregion


        #region Servivces
        /// <summary>
        /// Get All Land Lots
        /// </summary>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<LandLotDto> result = _landLotServices.GetAll();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result
            };

            return Ok(response);
        }

        /// <summary>
        /// Get All Land Lots By Guide Number
        /// </summary>
        /// <param name="guideNumber"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpGet]
        [Route("GetByGuideNumber")]
        public IActionResult GetByGuideNumber(string guideNumber)
        {
            LandLotDto result = _landLotServices.GetByGuideNumber(guideNumber);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result
            };

            return Ok(response);
        }

        /// <summary>
        /// Insert New Land Lot
        /// </summary>
        /// <param name="lot"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(AddLandLotDto lot)
        {
            IActionResult action;
            bool result = await _landLotServices.Insert(lot);
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
        /// Update Land Lot
        /// </summary>
        /// <param name="lot"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateLandLotDto lot)
        {
            IActionResult action;
            bool result = await _landLotServices.Update(lot);
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
        /// Delete Land Lot
        /// </summary>
        /// <param name="idLandLot"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int idLandLot)
        {
            IActionResult action;
            bool result = await _landLotServices.Delete(idLandLot);
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
