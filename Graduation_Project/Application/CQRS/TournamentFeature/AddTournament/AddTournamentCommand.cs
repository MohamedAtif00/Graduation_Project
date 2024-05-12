using Graduation_Project.Application.Abstraction;

namespace Graduation_Project.Application.CQRS.TournamentFeature.AddTournament
{
    public record AddTournamentCommand(string courtName, string day, string time) :ICommand;
    
    
}
