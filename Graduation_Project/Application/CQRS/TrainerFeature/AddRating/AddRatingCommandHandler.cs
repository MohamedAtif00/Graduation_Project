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
                var ratings = await _unitOfWork.TrainerRatingRepository.GetAllRatingByTrainerId(TrainerId.Create(request.trainerId));

                if (ratings.Where(x => x.username == request.username).Any()) return Result.Error("this user already make rating");

                var rating = await _unitOfWork.TrainerRatingRepository.Add(TrainerRating.Create(TrainerId.Create(request.trainerId),request.Rating,request.username));

                int saving1 = await _unitOfWork.save();

                if (saving1 == 0)
                {
                    return Result.Error("No change.");
                }

                // Recalculate the average rating
                ratings = await _unitOfWork.TrainerRatingRepository.GetAllRatingByTrainerId(TrainerId.Create(request.trainerId));
                double sum = ratings.Sum(r => Convert.ToInt32(r.rating));
                double avg = sum / ratings.Count();

                // Scale average rating to a 5-point scale if not already on it
                avg = Math.Min(avg, 5.0);

                var trainer = await _unitOfWork.TrainerRepository.GetById(TrainerId.Create(request.trainerId));
                trainer.ModifyAvgRating(avg);

                await _unitOfWork.TrainerRepository.Update(trainer);

                int saving2 = await _unitOfWork.save();

                if (saving2 == 0)
                {
                    return Result.Error("No change.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error("System error");
            }
        }
    }
}
