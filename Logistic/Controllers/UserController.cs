using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.User;
using Logistic.Domain.DTO;
using Logistic.Domain.Services.Logistic.Interfaces;
using Logistic.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Logistic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(CustomValidationFilterAttribute))]
    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUserServices _userServices;
        #endregion

        #region Builder
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Services
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDto login)
        {
            TokenDto result = _userServices.Login(login);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = result,
                Message = string.Empty
            };

            return Ok(response);
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="register"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(AddUserDto register)
        {
            IActionResult action;
            bool result = await _userServices.RegisterUser(register);
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
        #endregion
    }
}
