using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.AddRating
{
    public record AddRatingCommand(Guid trainerId,Rating  Rating,string username):ICommand;
    
    
}
