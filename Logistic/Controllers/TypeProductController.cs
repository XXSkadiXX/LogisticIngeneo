using Logistic.Common.Resources;
using Logistic.Domain.DTO;
using Logistic.Domain.DTO.Logistic.TypeProduct;
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
    public class TypeProductController : ControllerBase
    {
        #region Attributes
        private readonly ITypeProductServices _typeProductServices;
        #endregion

        #region Builder
        public TypeProductController(ITypeProductServices typeProductServices)
        {
            _typeProductServices = typeProductServices;
        }
        #endregion

        #region Services
        /// <summary>
        /// Get All Type of Products
        /// </summary>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllTypeProduct()
        {
            List<TypeProductDto> list = _typeProductServices.GetAllTypeProduct();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };


            return Ok(response);
        }

        /// <summary>
        /// Insert New Type Products
        /// </summary>
        /// <param name="addTypeProduct"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> InsertTypeProduct(AddTypeProductDto addTypeProduct)
        {
            IActionResult action;
            bool result = await _typeProductServices.InsertTypeProduct(addTypeProduct);
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
        /// Update Type of Product
        /// </summary>
        /// <param name="typeProduct"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateTypeProduct(TypeProductDto typeProduct)
        {
            IActionResult action;
            bool result = await _typeProductServices.UpdateTypeProduct(typeProduct);
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
        /// Delete Type of Product
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
        public async Task<IActionResult> DeleteTypeProduct(int id)
        {
            IActionResult action;
            bool result = await _typeProductServices.DeleteTypeProduct(id);
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
