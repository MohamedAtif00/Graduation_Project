using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;

namespace Graduation_Project.Domain.Repsitory.TournamentRepo
{
    public interface ITournamentRepository : IGenericRepository<Tournament,TournamentId>
    {
    }
}
