using Graduation_Project.Domain.Entity.RefreshTokenDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.DomainConfig.RefreshTokenConfig
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.value, x => RefreshTokenId.Create(x));


        }
    }
}
