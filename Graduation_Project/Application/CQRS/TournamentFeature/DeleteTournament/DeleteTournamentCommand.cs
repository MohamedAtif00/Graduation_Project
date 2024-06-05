using Graduation_Project.Application.Abstraction;

namespace Graduation_Project.Application.CQRS.TournamentFeature.DeleteTournament
{
    public record DeleteTournamentCommand(Guid tournamentId):ICommand;
    
    
}
