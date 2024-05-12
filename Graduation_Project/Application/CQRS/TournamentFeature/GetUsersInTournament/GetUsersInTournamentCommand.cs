using Graduation_Project.Application.Abstraction;
using Graduation_Project.Application.DTOs.User;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.TournamentFeature.GetUsersInTournament
{
    public record GetUsersInTournamentCommand(Guid id):ICommand<List<UserDto>>;
    
    
}
