using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;

namespace Graduation_Project.Application.CQRS.TournamentFeature.DeleteTournament
{
    public class DeleteTournamentCommandHandler : ICommandHandler<DeleteTournamentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTournamentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tournament = await _unitOfWork.TournamentRepository.GetById(TournamentId.Create(request.tournamentId));
                 await _unitOfWork.TournamentRepository.Delete(tournament);

                int saving = await _unitOfWork.save();
                if (saving == 0) return Result.Error("no changes");

                return Result.Success();

            } catch (Exception ex) {
                return Result.Error("System Error");
            }
        }
    }
}
