using Graduation_Project.Domain.Abstraction;

namespace Graduation_Project.Domain.Entity.TournamentDomain
{
    public class TournamentId : ValueObjectId
    {
        protected TournamentId(Guid id) : base(id)
        {
        }

        public static TournamentId Create(Guid id)
        { 
            return new TournamentId(id);
        }

        public static TournamentId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}