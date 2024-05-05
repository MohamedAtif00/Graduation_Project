using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Repsitory.TrainerRepo;
using Graduation_Project.Infrastructure.Data;
using Graduation_Project.Infrastructure.DomainConfig;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.Repositories
{
    public class TrainerRatingRepository : GenericRepository<TrainerRating, TrainerRatingId>,ITrainerRatingRepository
    {
        public TrainerRatingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<TrainerRating>> GetAllRatingByTrainerId(TrainerId id)
        {
            return await _context.trainerRatings.Where(x => x.trainerId == id).ToListAsync();
        }
    }
}
