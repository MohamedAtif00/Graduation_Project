using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.RefreshTokenDomain;
using Graduation_Project.Domain.Repsitory.RefreshTokenRepo;
using Graduation_Project.Infrastructure.Data;
using Graduation_Project.Infrastructure.DomainConfig;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken, RefreshTokenId>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken> GetRefreshTokenByUserId(Guid userId)
        {
            return await _context.refreshTokens.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
