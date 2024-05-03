using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.GetSingleUser
{
    public record GetSingleUserQuery(Guid userId):IQuery<User>;
    
    
}
