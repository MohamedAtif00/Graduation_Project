namespace Graduation_Project.Application.DTOs.Authentication
{
    public class JwtSettings
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audiance { get; set; }
        public int AccessTokeExpiryMinutes { get; set; }
        public int RefreshTokenExpiryMinutes { get; set; }
    }
}
