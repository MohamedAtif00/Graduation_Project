using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.RefreshTokenDomain;

namespace Graduation_Project.Domain.Entity.UserDomain
{
    public class UserId : ValueObjectId
    {
        protected UserId(Guid id) : base(id)
        {
        }

        public static UserId Create(Guid id)
        {
            return new(id);
        }

        public static UserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}