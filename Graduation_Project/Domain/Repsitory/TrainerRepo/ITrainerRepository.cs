using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Domain.Repsitory.TrainerRepo
{
    public interface ITrainerRepository : IGenericRepository<Trainer,TrainerId>
    {
    }
}
