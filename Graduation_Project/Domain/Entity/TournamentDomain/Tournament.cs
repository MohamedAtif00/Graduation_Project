using Graduation_Project.Domain.Abstraction;

namespace Graduation_Project.Domain.Entity.TournamentDomain
{
    public class Tournament : Entity<TournamentId>
    {
        public Tournament(TournamentId id, string courtName, string day, string time) : base(id)
        {
            CourtName = courtName;
            Day = day;
            Time = time;
        }
        public string CourtName { get;private set; }

        public string Day { get; private set; }
        public string Time { get; private set; }

        public static Tournament Create(string courtName, string day, string time)
        {
            return new(TournamentId.CreateUnique(),courtName,day,time);
        }

        public void Update(string courtName, string day, string time)
        {
            CourtName = courtName;
            Day = day;
            Time = time;
        }

    }
}
