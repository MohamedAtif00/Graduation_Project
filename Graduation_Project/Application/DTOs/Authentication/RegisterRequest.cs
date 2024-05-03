using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.DTOs.Authentication
{
    public record RegisterRequest(string email, string username, string password, DateTime birthDate, string nationalId, string city, string phone,IFormFile image, Gender gender, TennisCourt tennisCourt, Guid trainerId, bool hasHealthCondition,string? detials = null);
}
