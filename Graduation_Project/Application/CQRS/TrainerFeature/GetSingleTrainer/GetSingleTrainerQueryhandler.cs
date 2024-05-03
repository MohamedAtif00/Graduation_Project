using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.GetSingleTrainer
{
    public class GetSingleTrainerQueryhandler : IQueryHandler<GetSingleTrainerQuery, Trainer>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleTrainerQueryhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Trainer>> Handle(GetSingleTrainerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var trainer = await _unitOfWork.TrainerRepository.GetById(TrainerId.Create(request.trainerId));

                if (trainer == null) return Result.Error("Trainer is not exist");

                return Result.Success(trainer);

            }catch (Exception ex)
            {
                return Result.Error("System Error");
            }
        }
    }
}
