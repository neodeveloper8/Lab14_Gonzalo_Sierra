using Microsoft.AspNetCore.Mvc;
using Tuapp.Application.DTOs;
using Tuapp.Application.Interfaces;

namespace TuApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok("Usuario registrado correctamente");
        }
    }
}
