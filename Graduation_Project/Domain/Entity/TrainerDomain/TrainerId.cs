using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.RefreshTokenDomain;

namespace Graduation_Project.Domain.Entity.TrainerDomain
{
    public class TrainerId : ValueObjectId
    {
        protected TrainerId(Guid id) : base(id)
        {
        }

        public static TrainerId Create(Guid id)
        {
            return new(id);
        }

        public static TrainerId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}