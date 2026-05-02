using Microsoft.AspNetCore.Mvc;
using ShopNest.Core.Common;
using System.Security.Claims;

namespace Shopnest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected int CurrentUserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        protected string CurrentUserRole =>
            User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        protected IActionResult OkResponse<T>(T data, string? message = null) =>
            Ok(ApiResponse<T>.Success(data, message));

        protected IActionResult CreatedResponse<T>(T data, string? message = null) =>
            StatusCode(201, ApiResponse<T>.Success(data, message));
    }
}
