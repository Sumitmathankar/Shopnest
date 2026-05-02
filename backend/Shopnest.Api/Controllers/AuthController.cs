using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopNest.Core.DTOs;
using ShopNest.Core.IServices;
namespace Shopnest.Api.Controllers
{
    [Route("auth/[controller]")]
    [ApiController]
    public class AuthController: BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return CreatedResponse(result, "Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult>Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return OkResponse(result, "Login successful.");
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
        {
            var result = await _authService.RefreshTokenAsync(dto);
            return OkResponse(result);
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            await _authService.RevokeTokenAsync(CurrentUserId);
            return OkResponse<object>(null!, "Token revoked.");
        }
    }
}
