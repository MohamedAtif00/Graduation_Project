using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.CQRS.UserFeature.AddTrainer
{
    public class AddTrainerAndCourtCommandHandler : ICommandHandler<AddTrainerAndCourtCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTrainerAndCourtCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AddTrainerAndCourtCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetById(UserId.Create(request.userId));

                user.AddTrainerAndCourt(request.TennisCourt,TrainerId.Create(request.trainerId));

                await _unitOfWork.UserRepository.Update(user);

                int save = await _unitOfWork.save();

                if (save == 0) return Result.Error("no change");

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.ToString());
            }

        }
    }
}
