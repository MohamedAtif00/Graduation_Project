using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Application.DTOs.Authentication;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.RefreshTokenDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Graduation_Project.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly PasswordHasher<IdentityUser> _passwordHasher;

        public AuthenticationService(UserManager<IdentityUser<Guid>> userManager, IOptions<JwtSettings> options, IUnitOfWork unitOfWork, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _jwtSettings = options.Value;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }

        public async Task<Result<JwtTokenDto>> Register(string Username, string Email, string Password, string Role)
        {
            try
            {
                var JwtTokenDto = new JwtTokenDto();
                if (Username == null || Email == null || Password == null)
                {
                    return Result.Error("Invalid input");
                }
                if (string.IsNullOrEmpty(Username))
                {
                    return Result.Error("Username is required");
                }
                var existingUser = await _userManager.FindByNameAsync(Username);
                if (existingUser != null && _userManager.GetRolesAsync(existingUser).Result[0] == Role)
                {
                    return Result.Error("Username is already in use");
                }

                var newUser = new IdentityUser<Guid> { UserName = Username, Email = Email };
                var result = await _userManager.CreateAsync(newUser, Password);

                if (!result.Succeeded)
                {
                    return Result.Error("User registration failed");
                }
                // Add user to role
                if (!string.IsNullOrEmpty(Role))
                {
                    var roleExists = await _roleManager.RoleExistsAsync(Role);
                    if (!roleExists)
                    {
                        return Result.Error($"Role '{Role}' does not exist");
                    }
                    IdentityResult addToRoleResult = new();

                    var existingUserRoles = await _userManager.GetRolesAsync(newUser);
                    var existingUserRole = existingUserRoles.FirstOrDefault();

                    if (existingUserRole == null)
                    {
                        addToRoleResult = await _userManager.AddToRoleAsync(newUser, Role);
                    }
                    else if (existingUserRole != Role)
                    {
                        addToRoleResult = await _userManager.AddToRoleAsync(newUser, Role);
                    }
                    else
                    {
                        addToRoleResult = IdentityResult.Failed();
                    }


                    //if (_userManager.GetRolesAsync(existingUser).Result[0] != Role || _userManager.GetRolesAsync(existingUser).Result[0] == null)
                    //    addToRoleResult = await _userManager.AddToRoleAsync(newUser, Role);


                    if (!addToRoleResult.Succeeded && addToRoleResult != null)
                    {
                        return Result.Error($"Failed to assign role '{Role}' to the user");
                    }
                }

                // Generate Claims Identity
                var claimIdentity = GenerateClaimsIdentity(newUser, Role);
                // Generate JWT Token
                var jwtToken = GenerateAccessToken(claimIdentity.Result);
                // Generate Refresh Token
                var refreshToken = await GenerateRefreshToken(newUser);
                // Set these into JwtTokenDto
                JwtTokenDto.UserId = newUser.Id;
                JwtTokenDto.Username = newUser.UserName;
                JwtTokenDto.Role = _userManager.GetRolesAsync(newUser).Result.FirstOrDefault() ?? "";
                JwtTokenDto.JwtToken = jwtToken.Result;
                JwtTokenDto.RefreshToken = refreshToken;
                JwtTokenDto.Error = string.Empty;

                return Result.Success(JwtTokenDto);

            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }


        public async Task<Result<JwtTokenDto>> Login(string email, string password, string role)
        {
            try
            {
                IdentityUser<Guid> user = new();
                var jwttokenDto = new JwtTokenDto();
                if (email == null || password == null)
                {
                    return Result.Error("invalid input");
                }
                if (string.IsNullOrEmpty(email))
                {
                    return Result.Error("username is required");
                }

                user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return Result.NotFound("username is not exist");
                }
                var userRole = await _userManager.GetRolesAsync(user);

                if (userRole == null || userRole[0] != role) { return Result.Error($"this user doesn't has {role} role"); }


                //var user = await _userManager.FindByNameAsync(loginDto.Username);
                //if (user == null) return Result.NotFound("this user is not exist");
                var locked = await _userManager.IsLockedOutAsync(user);
                if (locked) return Result.Error("Account lockedout");
                var passwordValid = await _userManager.CheckPasswordAsync(user, password);
                if (!passwordValid)
                {
                    await _userManager.AccessFailedAsync(user);
                    return Result.Error("username or password is not valid");
                }

                //Generate Claims Identity
                var claimIdentity = await GenerateClaimsIdentity(user, role);
                //Generate JWT Token
                var jwtToken = await GenerateAccessToken(claimIdentity);
                //Generate Refresh Toekn
                var refreshToken = await GenerateRefreshToken(user);
                //Set these into jwwtTokenVm
                jwttokenDto.JwtToken = jwtToken;
                jwttokenDto.Role = role;
                jwttokenDto.UserId = user.Id;
                jwttokenDto.Username = user.UserName??"";
                jwttokenDto.RefreshToken = refreshToken;
                jwttokenDto.Error = string.Empty;

                return Result.Success(jwttokenDto);

            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }

        public async Task<Result<string>> CheckUsername(string username)
        {
            var result = await _userManager.FindByNameAsync(username);

            if (result == null) return Result.NotFound("this username is not exist");
            return Result.Success(result.UserName);
        }

        public async Task<ClaimsIdentity> GenerateClaimsIdentity(IdentityUser<Guid> user, string role)
        {

            var secondsFromUnixEpoch = (DateTime.UtcNow - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
            var unixEpochDatestr = ((long)Math.Round(secondsFromUnixEpoch)).ToString();

            var claims = new List<Claim>
            {
                new Claim("userid",user.Id.ToString()),
                new Claim("username",user.UserName),
                new Claim("email",user.Email),
                new Claim("password",user.PasswordHash),
                new Claim(ClaimTypes.Role,role),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,unixEpochDatestr)
            };

            return new ClaimsIdentity(new GenericIdentity(user.UserName, "token"), claims);
        }

        public async Task<string> GenerateAccessToken(ClaimsIdentity claimsIdentity)
        {
            var jwtToken = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audiance,
                    claimsIdentity.Claims,
                    DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.AccessTokeExpiryMinutes)),
                    DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiryMinutes)),
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<string> GenerateRefreshToken(IdentityUser<Guid> newUser)
        {
            // Get the user's ID
            var userId = newUser.Id;

            // Check if a refresh token exists for the user
            var existingRefreshToken = await _unitOfWork.RefreshTokenRepository.GetRefreshTokenByUserId(userId);

            // If a refresh token exists, delete it
            if (existingRefreshToken != null)
            {
                await _unitOfWork.RefreshTokenRepository.Delete(existingRefreshToken);
            }

            // Create a new refresh token
            var issuedUtc = DateTime.UtcNow;
            var expiresUtc = issuedUtc.AddMinutes(Convert.ToInt32(_jwtSettings.RefreshTokenExpiryMinutes));
            var refreshToken = RefreshToken.Create(userId, issuedUtc, expiresUtc);

            // Add the new refresh token to the database
            await _unitOfWork.RefreshTokenRepository.Add(RefreshToken.Create(refreshToken.UserId, refreshToken.IssuedUtc, refreshToken.ExpiresUtc));

            // Return the ID of the new refresh token
            return refreshToken.Id.value.ToString() ?? string.Empty;
        }

        public async Task<Result> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (user == null) { return Result.NotFound("this user is not exist"); }
            var confirm = await _userManager.ConfirmEmailAsync(user, result);
            if (confirm == IdentityResult.Success)
            {
                return Result.Success();
            }
            return Result.Error();
        }




    }
}
