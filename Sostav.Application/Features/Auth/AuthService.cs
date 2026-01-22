using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sostav.Application.Common.Interfaces;
using Sostav.Application.Common.Models;
using Sostav.Application.Common.Settings;
using Sostav.Application.Features.Auth.Dtos;
using Sostav.Domain.Entities;
using Sostav.Domain.Enums;
using LoginRequest = Sostav.Application.Features.Auth.Dtos.LoginRequest;
using RegisterRequest = Sostav.Application.Features.Auth.Dtos.RegisterRequest;

namespace Sostav.Application.Features.Auth;

public class AuthService : IAuthService
{
    private readonly IDataContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IDataContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        // Check if phone already exists
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Phone == request.Phone);

        if (existingUser != null)
        {
            return Result.Failure<AuthResponse>("This phone is already registered"); // Bu telefon raqami allaqachon ro'yxatdan o'tgan
        }

        // Create new user
        var user = new User
        {
            FullName = request.FullName,
            Phone = request.Phone,
            PasswordHash = _passwordHasher.Hash(request.Password),
            TelegramUsername = request.TelegramUsername,
            Role = UserRole.Player,
            City = "Tashkent",
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Generate token
        var token = _jwtTokenService.GenerateToken(user);

        return new AuthResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Phone = user.Phone,
            TelegramUsername = user.TelegramUsername,
            Role = user.Role.ToString(),
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes)
        };
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Phone == request.Phone && u.IsActive);

        if (user == null)
        {
            return Result.Failure<AuthResponse>("Invalid phone or password "); // Telefon raqami yoki parol noto'g'ri
        }

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return Result.Failure<AuthResponse>("Invalid phone or password"); // Telefon raqami yoki parol noto'g'ri
        }

        var token = _jwtTokenService.GenerateToken(user);

        return new AuthResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Phone = user.Phone,
            TelegramUsername = user.TelegramUsername,
            Role = user.Role.ToString(),
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes)
        };
    }

    public async Task<Result<UserDto>> GetCurrentUserAsync(Guid userId)
    {
        var user = await _context.Users
            .Where(u => u.Id == userId && u.IsActive)
            .Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Phone = u.Phone,
                TelegramUsername = u.TelegramUsername,
                AvatarUrl = u.AvatarUrl,
                City = u.City,
                Role = u.Role.ToString(),
                CreatedAt = u.CreatedAt,
                GamesPlayed = u.GameParticipations.Count(gp => gp.Status == ParticipantStatus.Confirmed),
                GamesOrganized = u.OrganizedGames.Count
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return Result.Failure<UserDto>("User not found"); // Foydalanuvchi topilmadi
        }

        return user;
    }
}
