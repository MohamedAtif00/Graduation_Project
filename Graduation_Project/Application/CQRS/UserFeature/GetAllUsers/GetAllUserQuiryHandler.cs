using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.GetAllUsers
{
    public class GetAllUserQuiryHandler : IQueryHandler<GetAllUserQuery, List<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUserQuiryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<User>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAll();

                return Result.Success(users);
            }catch (Exception ex)
            {
                return Result.CriticalError("System Error");
            }
        }
    }
}
