using Logistic.Common.Resources;
using Logistic.Domain.DTO;
using Logistic.Domain.DTO.Logistic.LandLot;
using Logistic.Domain.DTO.Logistic.Warehouse;
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
    public class WarehouseController : ControllerBase
    {
        #region Attributes
        private readonly IWarehouseServices _warehouseServices;
        #endregion

        #region Builder
        public WarehouseController(IWarehouseServices warehouseServices)
        {
            _warehouseServices = warehouseServices;
        }
        #endregion

        #region Services
        /// <summary>
        /// Get All Warehouses
        /// </summary>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllWarehouses()
        {
            List<WarehouseDto> list = _warehouseServices.GetAllWarehouses();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };


            return Ok(response);
        }

        /// <summary>
        /// Get All Warehouses By Country
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
        public IActionResult GetAllWarehousesByCountry(int countryId)
        {
            List<WarehouseDto> list = _warehouseServices.GetAllWarehousesByCountry(countryId);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };


            return Ok(response);
        }

        /// <summary>
        /// Insert New Warehouse
        /// </summary>
        /// <param name="addWarehouse"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> InsertWarehouse(AddWarehouseDto addWarehouse)
        {
            IActionResult action;
            bool result = await _warehouseServices.InsertWarehouse(addWarehouse);
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
        /// 
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateWarehouse(UpdateWareHouseDto warehouse)
        {
            IActionResult action;
            bool result = await _warehouseServices.UpdateWarehouse(warehouse);
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
        /// Delete Warehouse
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
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            IActionResult action;
            bool result = await _warehouseServices.DeleteWarehouse(id);
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
