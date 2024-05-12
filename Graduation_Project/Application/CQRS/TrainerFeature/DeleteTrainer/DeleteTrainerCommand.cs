using Graduation_Project.Application.Abstraction;

namespace Graduation_Project.Application.CQRS.TrainerFeature.DeleteTrainer
{
    public record DeleteTrainerCommand(Guid id):ICommand;
    
    
}
