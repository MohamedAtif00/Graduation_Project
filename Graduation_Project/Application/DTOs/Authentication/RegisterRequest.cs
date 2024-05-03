using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.DTOs.Authentication
{
    public record RegisterRequest(string firstName ,string lastName,string email, string password, DateTime birthDate, string nationalId, string city, string phone,IFormFile image, Gender gender, TennisCourt tennisCourt,TennisExp TennisExp,TimeSessionDto timeSession, Guid trainerId, bool hasHealthCondition,string? detials = null);
}
