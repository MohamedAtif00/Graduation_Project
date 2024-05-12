using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.AddToTournament
{
    public class AddToTournamentCommandHandler : ICommandHandler<AddToTournamentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddToTournamentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddToTournamentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetById(UserId.Create(request.userId));

                if (user == null) return Result.Error("user is not exist");

                user.AddTournament(TournamentId.Create(request.TournamentId));

                await _unitOfWork.UserRepository.Update(user);

                int saving = await _unitOfWork.save();
                if (saving == 0) return Result.Error("No change");

                return Result.Success();
            }catch (Exception ex)
            {
                return Result.Error("System Error"); 
            }
        }
    }
}
