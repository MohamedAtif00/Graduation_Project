using Graduation_Project.Domain.Abstraction;
using Graduation_Project.Domain.Entity.RefreshTokenDomain;

namespace Graduation_Project.Domain.Repsitory.RefreshTokenRepo
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken, RefreshTokenId>
    {
        Task<RefreshToken> GetRefreshTokenByUserId(Guid userId);
    }
}
