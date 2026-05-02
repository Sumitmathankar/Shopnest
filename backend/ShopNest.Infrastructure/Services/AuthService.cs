using ShopNest.Core.DTOs;
using ShopNest.Core.Entities;
using ShopNest.Core.IServices;
using ShopNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNest.Infrastructure.Services
{
    public class AuthService :IAuthService
    {
        private readonly ShopnestDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(ShopnestDbContext shopnestDbContext,ITokenService tokenService)
        {
            _context = shopnestDbContext;
            _tokenService= tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists)
                throw new InvalidOperationException("Email is already registered.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Customer"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return BuildAuthResponse(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return BuildAuthResponse(user);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.RefreshToken == dto.RefreshToken &&
                    u.RefreshTokenExpiryTime > DateTime.UtcNow);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");

            return BuildAuthResponse(user);
        }

        public async Task RevokeTokenAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId)
                ?? throw new KeyNotFoundException("User not found.");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _context.SaveChangesAsync();
        }

        private AuthResponseDto BuildAuthResponse(User user)
        {
            var accessToken = _tokenService.CreateAccessToken(user);
            var refreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _context.SaveChanges();

            return new AuthResponseDto(
                user.Id, user.Name, user.Email,
                user.Role, accessToken, refreshToken);
        }
    }
}
