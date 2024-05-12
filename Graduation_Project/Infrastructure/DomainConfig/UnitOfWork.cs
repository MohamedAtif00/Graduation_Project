using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Repsitory.RefreshTokenRepo;
using Graduation_Project.Domain.Repsitory.TournamentRepo;
using Graduation_Project.Domain.Repsitory.TrainerRepo;
using Graduation_Project.Domain.Repsitory.UserRepo;
using Graduation_Project.Infrastructure.Data;

namespace Graduation_Project.Infrastructure.DomainConfig
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, ITrainerRepository trainerRepository, ITrainerRatingRepository trainerRatingRepository, ITournamentRepository tournamentRepository)
        {
            _applicationDbContext = applicationDbContext;

            RefreshTokenRepository = refreshTokenRepository;
            UserRepository = userRepository;
            TrainerRepository = trainerRepository;
            TrainerRatingRepository = trainerRatingRepository;
            TournamentRepository = tournamentRepository;
        }



        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IUserRepository UserRepository { get; }
        public ITrainerRepository TrainerRepository { get; }
        public ITrainerRatingRepository TrainerRatingRepository { get; }
        public ITournamentRepository TournamentRepository { get; }

        public async Task<int> save()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }
    }

}
