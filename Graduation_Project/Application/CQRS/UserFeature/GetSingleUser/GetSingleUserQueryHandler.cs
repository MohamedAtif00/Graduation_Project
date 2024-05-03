using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.GetSingleUser
{
    public class GetSingleUserQueryHandler : IQueryHandler<GetSingleUserQuery, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<User>> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetById(UserId.Create(request.userId));

                if (user == null) return Result.Error("this user is not exist");

                return Result.Success(user);
            }catch (Exception ex)
            {
                return Result.Error("Syetem Error");
            }
        }
    }
}
