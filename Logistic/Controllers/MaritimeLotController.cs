using Logistic.Common.Resources;
using Logistic.Domain.DTO;
using Logistic.Domain.DTO.Logistic.MaritimeLot;
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
    public class MaritimeLotController : ControllerBase
    {
        #region Attributes
        private readonly IMaritimeLotServices _maritimeLotServices;
        #endregion

        #region Builder
        public MaritimeLotController(IMaritimeLotServices maritimeLotServices)
        {
            _maritimeLotServices = maritimeLotServices;
        }
        #endregion


        #region Servivces
        /// <summary>
        /// Get All Maritime Lots
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
            List<MaritimeLotDto> result = _maritimeLotServices.GetAll();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result
            };

            return Ok(response);
        }

        /// <summary>
        /// Get All Maritime Lots By Guide Number
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
            MaritimeLotDto result = _maritimeLotServices.GetByGuideNumber(guideNumber);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result
            };

            return Ok(response);
        }

        /// <summary>
        /// Insert new Maritime Lot
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
        public async Task<IActionResult> Insert(AddMaritimeLotDto lot)
        {
            IActionResult action;
            bool result = await _maritimeLotServices.Insert(lot);
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
        /// Update Maritime Lot
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
        public async Task<IActionResult> Update(UpdateMaritimeLotDto lot)
        {
            IActionResult action;
            bool result = await _maritimeLotServices.Update(lot);
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
        /// Deleter Maritime Lot
        /// </summary>
        /// <param name="idMaritimeLot"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int idMaritimeLot)
        {
            IActionResult action;
            bool result = await _maritimeLotServices.Delete(idMaritimeLot);
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
