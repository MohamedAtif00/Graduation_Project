using Ardalis.Result;
using Graduation_Project.Application.DTOs.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Graduation_Project.Application.Abstraction
{
    public interface IAuthenticationService
    {
        Task<Result<string>> CheckUsername(string username);
        Task<Result> ConfirmEmail(string userId, string code);
        Task<string> GenerateAccessToken(ClaimsIdentity claimsIdentity);
        Task<ClaimsIdentity> GenerateClaimsIdentity(IdentityUser<Guid> user, string role);
        Task<string> GenerateRefreshToken(IdentityUser<Guid> newUser);
        Task<Result<JwtTokenDto>> Login(string username, string password, string role);
        Task<Result<JwtTokenDto>> Register(string Username, string Email, string Password, string Role);
    }
}
