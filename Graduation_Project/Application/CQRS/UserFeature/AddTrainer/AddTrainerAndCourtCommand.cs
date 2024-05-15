using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.AddTrainer
{
    public record AddTrainerAndCourtCommand(Guid userId,Guid trainerId,TennisCourt TennisCourt):ICommand<bool>;
   
    
}
