using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.AddUser
{
    public record AddUserCommand(Guid userId,string firstName,string secondName, DateTime birthDate, string nationalId, string city, string phone, IFormFile image,Gender gender, TennisCourt tennisCourt,TennisExp TennisExp,TimeSession    timeSession, Guid trainerId,bool hasHealthCondition,string healthDetails = null) :ICommand;
    
    
}
