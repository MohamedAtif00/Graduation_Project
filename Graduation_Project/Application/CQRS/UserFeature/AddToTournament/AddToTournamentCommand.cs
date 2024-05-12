using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.AddToTournament
{
    public record AddToTournamentCommand(Guid TournamentId,Guid userId):ICommand;
    
   
}
