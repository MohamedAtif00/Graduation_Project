using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.AddUser
{
    public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                byte[] file;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    request.image.CopyTo(memoryStream);
                    file = memoryStream.ToArray();
                }

                var result = await _unitOfWork.UserRepository.Add(User.Create(request.userId,request.birthDate,request.nationalId,request.city,request.phone,file,request.gender,request.tennisCourt,TrainerId.Create(request.trainerId),request.hasHealthCondition?HealthCondition.Create(request.healthDetails):null));

                int saving = await _unitOfWork.save();

                if (saving == 0) return Result.Error("no changes");

                return Result.Success();

            }catch (Exception ex)
            {
                return Result.CriticalError("System Error");
            }
        }
    }
}
