using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.GetAllRating
{
    public record GetAllRatingQuery(Guid trainerId):IQuery<List<TrainerRating>>;
    
    
}
