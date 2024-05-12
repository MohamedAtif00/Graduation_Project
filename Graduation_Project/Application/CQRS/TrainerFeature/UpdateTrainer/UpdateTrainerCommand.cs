using Graduation_Project.Application.Abstraction;

namespace Graduation_Project.Application.CQRS.TrainerFeature.UpdateTrainer
{
    public record UpdateTrainerCommand(Guid id,string username, DateTime birthdate, int exp, string specia, string phone, string email, IFormFile image) :ICommand;
    
    
}
