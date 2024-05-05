using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.GetAllRating
{
    public class GetAllRatingQueryHandler : IQueryHandler<GetAllRatingQuery, List<TrainerRating>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRatingQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<TrainerRating>>> Handle(GetAllRatingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var trainers = await _unitOfWork.TrainerRatingRepository.GetAllRatingByTrainerId(TrainerId.Create(request.trainerId));

                return Result.Success(trainers);
                
            }catch (Exception ex)
            {
                return Result.Error("System Error");
            }
        }
    }
}
