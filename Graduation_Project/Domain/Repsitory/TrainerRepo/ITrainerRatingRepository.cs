using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Domain.Repsitory.TrainerRepo
{
    public interface ITrainerRatingRepository : IGenericRepository<TrainerRating, TrainerRatingId>
    {
        Task<List<TrainerRating>> GetAllRatingByTrainerId(TrainerId id);
    }
}
