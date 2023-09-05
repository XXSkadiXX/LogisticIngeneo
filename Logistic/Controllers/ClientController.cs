using Logistic.Common.Resources;
using Logistic.Domain.DTO;
using Logistic.Domain.DTO.Logistic.Client;
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
    public class ClientController : ControllerBase
    {
        #region Attributes
        private readonly IClientServices _clientServices;
        #endregion

        #region Builder
        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }
        #endregion

        #region Services
        /// <summary>
        /// Get All Clients
        /// </summary>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllClients()
        {
            List<ClientDto> list = _clientServices.GetAllClients();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = list
            };


            return Ok(response);
        }

        /// <summary>
        /// Insert New Client
        /// </summary>
        /// <param name="addClient"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> InsertClient(AddClientDto addClient)
        {
            IActionResult action;
            bool result = await _clientServices.InsertClient(addClient);
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
        /// Update Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateClient(UpdateClientDto client)
        {
            IActionResult action;
            bool result = await _clientServices.UpdateClient(client);
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
        /// Delete Client
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
        public async Task<IActionResult> DeleteClient(int id)
        {
            IActionResult action;
            bool result = await _clientServices.DeleteClient(id);
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
