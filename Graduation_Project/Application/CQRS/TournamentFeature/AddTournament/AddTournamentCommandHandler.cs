using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;

namespace Graduation_Project.Application.CQRS.TournamentFeature.AddTournament
{
    public class AddTournamentCommandHandler : ICommandHandler<AddTournamentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTournamentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddTournamentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.TournamentRepository.Add(Tournament.Create(request.courtName,request.day,request.time));

                int saving = await _unitOfWork.save();

                if (saving == 0) return Result.Error("no changes");

                return Result.Success();
            }catch (Exception ex)
            {
                return Result.Error("System Error");
            }
        }
    }
}
