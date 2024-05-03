using Graduation_Project.Domain.Abstraction;

namespace Graduation_Project.Domain.Entity.RefreshTokenDomain
{
    public class RefreshToken : Entity<RefreshTokenId>
    {
        public Guid UserId { get; private set; }
        public DateTime IssuedUtc { get; private set; }
        public DateTime ExpiresUtc { get; private set; }

        public RefreshToken(RefreshTokenId id) : base(id)
        {

        }
        protected RefreshToken(RefreshTokenId id, Guid userId, DateTime IssuedUtc, DateTime ExpiresUtc) : base(id)
        {
            UserId = userId;
            this.IssuedUtc = IssuedUtc;
            this.ExpiresUtc = ExpiresUtc;
        }

        public static RefreshToken Create(Guid userId, DateTime IssuedUtc, DateTime ExpiresUtc)
        {
            return new(RefreshTokenId.CreateUnique(), userId, IssuedUtc, ExpiresUtc);
        }

        public void Update(Guid userId, DateTime IssuedUtc, DateTime ExpiresUtc)
        {
            this.UserId = userId;
            this.IssuedUtc = IssuedUtc;
            this.ExpiresUtc = ExpiresUtc;
        }
    }
}
