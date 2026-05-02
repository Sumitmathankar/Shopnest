using ShopNest.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNest.Core.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
        Task RevokeTokenAsync(int userId);
    }
}
