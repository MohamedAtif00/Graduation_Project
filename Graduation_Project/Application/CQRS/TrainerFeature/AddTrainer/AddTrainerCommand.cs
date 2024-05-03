using Graduation_Project.Application.Abstraction;

namespace Graduation_Project.Application.CQRS.TrainerFeature.AddTrainer
{
    public record AddTrainerCommand(string username, DateTime birthdate, int exp, string specia, string phone, string email) : ICommand;
    
    
}
