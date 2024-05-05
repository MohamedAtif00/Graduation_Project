using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.AddRating
{
    public class AddRatingCommandHandler : ICommandHandler<AddRatingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRatingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rating = await _unitOfWork.TrainerRatingRepository.Add(TrainerRating.Create(TrainerId.Create(request.trainerId),request.Rating,request.username));

                int saving = await _unitOfWork.save();

                if (saving == 0) return Result.Error("no Change");

                return Result.Success();
            }catch (Exception ex)
            {
                return Result.Error("System error");
            }
        }
    }
}
