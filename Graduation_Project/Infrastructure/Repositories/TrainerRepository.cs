using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Repsitory.TrainerRepo;
using Graduation_Project.Infrastructure.Data;
using Graduation_Project.Infrastructure.DomainConfig;

namespace Graduation_Project.Infrastructure.Repositories
{
    public class TrainerRepository : GenericRepository<Trainer, TrainerId>,ITrainerRepository
    {
        public TrainerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
