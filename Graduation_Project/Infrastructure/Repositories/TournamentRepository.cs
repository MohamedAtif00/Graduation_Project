using Graduation_Project.Domain.Entity.TournamentDomain;
using Graduation_Project.Domain.Repsitory.TournamentRepo;
using Graduation_Project.Infrastructure.Data;
using Graduation_Project.Infrastructure.DomainConfig;

namespace Graduation_Project.Infrastructure.Repositories
{
    public class TournamentRepository : GenericRepository<Tournament, TournamentId>, ITournamentRepository
    {
        public TournamentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
