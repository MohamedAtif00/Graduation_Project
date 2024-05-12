using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;

namespace Graduation_Project.Application.CQRS.TournamentFeature.GetAllTournament
{
    public record GetAllTournamentQuery():IQuery<List<Tournament>>;
    
    
}
