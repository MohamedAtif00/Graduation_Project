using Graduation_Project.Domain.Abstraction;

namespace Graduation_Project.Domain.Entity.UserDomain
{
    public class TimeSession : Entity<TimeSessionId>
    {
        public TimeSpan Time { get;private set; }
        public TimeSession(TimeSessionId id, TimeSpan time) : base(id)
        {
            Time = time;
        }

        public static TimeSession Create(TimeSpan time)
        {
            return new(TimeSessionId.CreateUnique(),time) ;
        }

        public void Update(TimeSpan time)
        {
            Time = time;
        }
    }
}
