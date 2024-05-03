namespace Graduation_Project.Application.DTOs.Authentication
{
    public record AllowAccessResponse(string userId, string username, string role, string emai, string token);
}
