using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNest.Core.DTOs
{
   // ── Request DTOs
    public record RegisterDto(
        string Name,
        string Email,
        string Password
    );

    public record LoginDto(
        string Email,
        string Password
    );

    public record RefreshTokenDto(
        string AccessToken,
        string RefreshToken
    );

    // ── Response DTOs 
    public record AuthResponseDto(
        int Id,
        string Name,
        string Email,
        string Role,
        string AccessToken,
        string RefreshToken
    );
}
