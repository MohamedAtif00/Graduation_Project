using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.DeleteTrainer
{
    public class DeleteTrainerCommandHandler : ICommandHandler<DeleteTrainerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTrainerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTrainerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var trainer = await _unitOfWork.TrainerRepository.GetById(TrainerId.Create(request.id));

                if (trainer == null) return Result.Error("trainer is not exist");

                await _unitOfWork.TrainerRepository.Delete(trainer);

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
