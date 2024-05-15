using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Application.DTOs.Authentication
{
    public record AddTrainerAndCourtDTO(Guid userId,Guid trainerId,TennisCourt TennisCourt);
    
    
}
