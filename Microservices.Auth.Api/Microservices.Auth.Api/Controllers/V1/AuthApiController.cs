using Microservices.Auth.Api.Models.Dto;
using Microservices.Auth.Api.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Auth.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;
        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMesage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMesage))
            { 
                _response.IsSuccess = false;
                _response.Message = errorMesage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "El nombre de usuario o la contraseña es incorrecto";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }
        [HttpPost("AssingRole")]
        public async Task<IActionResult> AssingRole([FromBody] RegistrationRequestDto model)
        {
            var assingRoleSuccess = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assingRoleSuccess)
            {
                _response.IsSuccess = false;
                _response.Message = "Error de Asignacion de Role";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
