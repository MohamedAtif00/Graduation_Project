using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.RefreshTokenDomain;

namespace Graduation_Project.Domain.Entity.UserDomain
{
    public class HealthCondition : ValueObject
    {
        public string Details { get;private set; }
        protected HealthCondition(string dateils) 
        {
            Details = dateils;
        }

        public static HealthCondition Create(string details = "")
        {
            return new(details);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Details;
        }
    }
}
