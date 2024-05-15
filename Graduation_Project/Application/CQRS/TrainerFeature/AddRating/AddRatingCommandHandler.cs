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

                int saving1 = await _unitOfWork.save();

                if (saving1 == 0) return Result.Error("no Change");

                var ratings = await _unitOfWork.TrainerRatingRepository.GetAllRatingByTrainerId(TrainerId.Create(request.trainerId));
                var arrayRatings = ratings.ToArray();

                double avg = 0;
                for (int i = 0; i < arrayRatings.Length; i++) // arr.Length should be the same as MaxRate
                {
                    avg += Convert.ToInt32( arrayRatings[i].rating) * (i + 1);
                }
                avg /= arrayRatings.Length;

                var trainer = await _unitOfWork.TrainerRepository.GetById(TrainerId.Create(request.trainerId));

                trainer.ModifyAvgRating(avg);

                await _unitOfWork.TrainerRepository.Update(trainer);

                int saving2 = await _unitOfWork.save();

                if (saving2 == 0) return Result.Error("no Change");

                return Result.Success();
            }catch (Exception ex)
            {
                return Result.Error("System error");
            }
        }
    }
}
