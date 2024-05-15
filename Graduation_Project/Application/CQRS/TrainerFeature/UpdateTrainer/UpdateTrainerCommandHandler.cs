using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.UpdateTrainer
{
    public class UpdateTrainerCommandHandler : ICommandHandler<UpdateTrainerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTrainerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTrainerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var trainer = await _unitOfWork.TrainerRepository.GetById(TrainerId.Create(request.id));

                if (trainer == null) return Result.Error("this trainer is not exist");

                byte[] file;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    request.image.CopyTo(memoryStream);
                    file = memoryStream.ToArray();
                }

                trainer.Update(request.username, file, request.birthdate, request.exp, request.specia, request.phone, request.email,request.price);

                 await _unitOfWork.TrainerRepository.Update(trainer);

                int saving = await _unitOfWork.save();

                if (saving == 0) return Result.Error("There is no changes");
                return Result.Success();
            }catch (Exception ex)
            {
                return Result.Error("System Error");
            }
        }
    }
}
