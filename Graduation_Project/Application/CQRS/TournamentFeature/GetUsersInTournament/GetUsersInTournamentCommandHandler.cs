using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Application.DTOs.User;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.TournamentFeature.GetUsersInTournament
{
    public class GetUsersInTournamentCommandHandler :ICommandHandler<GetUsersInTournamentCommand,List<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersInTournamentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<UserDto>>> Handle(GetUsersInTournamentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllUserInTournament(TournamentId.Create(request.id));


                return Result.Success(users);

            }catch (Exception ex)
            {
                return Result.Error("Sysetm Error");
            }
        }

    }
}
