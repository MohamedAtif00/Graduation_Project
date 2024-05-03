using Graduation_Project.Domain.Entity.UserDomain;
using Graduation_Project.Domain.Repsitory.UserRepo;
using Graduation_Project.Infrastructure.Data;
using Graduation_Project.Infrastructure.DomainConfig;

namespace Graduation_Project.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
