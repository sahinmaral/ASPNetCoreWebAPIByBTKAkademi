using Microsoft.AspNetCore.Mvc;

using StoreApp.Entities.DTOs;
using StoreApp.Presentation.ActionFilters;
using StoreApp.Services.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;


        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto dto)
        {
            var result = await _serviceManager.AuthenticationService.RegisterUser(dto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto dto)
        {
            var userExists = await _serviceManager.AuthenticationService.ValidateUser(dto);
            if (!userExists)
                return Unauthorized();

            var tokenDto = await _serviceManager.AuthenticationService.CreateAccessToken(true);

            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _serviceManager
                .AuthenticationService
                .CreateRefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }
    }
}
