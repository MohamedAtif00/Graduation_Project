using Graduation_Project.Domain.Abstraction;

namespace Graduation_Project.Domain.Entity.TrainerDomain
{
    public class TrainerRatingId : ValueObjectId
    {
        protected TrainerRatingId(Guid id) : base(id)
        {
        }

        public static TrainerRatingId Create(Guid id)
        {
            return new(id);
        }
        public static TrainerRatingId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
    }
}