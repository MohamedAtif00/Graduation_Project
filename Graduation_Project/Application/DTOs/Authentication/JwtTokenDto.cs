namespace Graduation_Project.Application.DTOs.Authentication
{
    public class JwtTokenDto
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public string Error { get; set; }
    }
}
