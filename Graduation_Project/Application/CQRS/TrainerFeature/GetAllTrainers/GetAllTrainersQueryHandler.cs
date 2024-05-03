using Ardalis.Result;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TrainerDomain;

namespace Graduation_Project.Application.CQRS.TrainerFeature.GetAllTrainers
{
    public class GetAllTrainersQueryHandler : IQueryHandler<GetAllTrainersQuery, List<Trainer>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTrainersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Trainer>>> Handle(GetAllTrainersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var trainers = await _unitOfWork.TrainerRepository.GetAll();

                return Result.Success(trainers);
            }catch (Exception ex)
            {
                return Result.Error("System Error");
            }
        }
    }
}
