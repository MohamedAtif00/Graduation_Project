using Graduation_Project.Application.DTOs.User;
using Graduation_Project.Domain.Entity.TournamentDomain;
using Graduation_Project.Domain.Entity.UserDomain;
using Graduation_Project.Domain.Repsitory.UserRepo;
using Graduation_Project.Infrastructure.Data;
using Graduation_Project.Infrastructure.DomainConfig;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<UserDto>> GetAllUserInTournament(TournamentId id)
        {
            return await _context.users.Where(x =>x.TournamentId == id).Select(x => new UserDto(x.FirstName,x.SecondName)).ToListAsync();
        }
    }
}
