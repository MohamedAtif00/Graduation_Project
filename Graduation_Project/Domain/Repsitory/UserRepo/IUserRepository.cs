using Graduation_Project.Application.DTOs.User;
using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.TournamentDomain;
using Graduation_Project.Domain.Entity.UserDomain;

namespace Graduation_Project.Domain.Repsitory.UserRepo
{
    public interface IUserRepository : IGenericRepository<User, UserId>
    {
        Task<List<UserDto>> GetAllUserInTournament(TournamentId id);
    }
}
