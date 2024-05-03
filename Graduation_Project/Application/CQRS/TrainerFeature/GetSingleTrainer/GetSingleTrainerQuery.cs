using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.GetSingleTrainer
{
    public record GetSingleTrainerQuery(Guid trainerId):IQuery<Trainer>;
    
    
}
