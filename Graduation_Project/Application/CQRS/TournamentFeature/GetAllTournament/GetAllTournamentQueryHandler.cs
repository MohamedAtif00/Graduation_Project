using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;

namespace Graduation_Project.Application.CQRS.TournamentFeature.GetAllTournament
{
    public class GetAllTournamentQueryHandler : IQueryHandler<GetAllTournamentQuery, List<Tournament>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTournamentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Tournament>>> Handle(GetAllTournamentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tournaments = await _unitOfWork.TournamentRepository.GetAll();

                return Result.Success(tournaments);
            }catch (Exception ex)
            {
                return Result.Error("System Error");
            }
        }
    }
}
