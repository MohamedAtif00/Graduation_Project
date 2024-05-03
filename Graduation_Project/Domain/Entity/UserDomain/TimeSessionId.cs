using Graduation_Project.Domain.Abstraction;

namespace Graduation_Project.Domain.Entity.UserDomain
{
    public class TimeSessionId : ValueObjectId
    {
        protected TimeSessionId(Guid id) : base(id)
        {
        }

        public static TimeSessionId Create(Guid id)
        {
            return new(id);
        }

        public static TimeSessionId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}