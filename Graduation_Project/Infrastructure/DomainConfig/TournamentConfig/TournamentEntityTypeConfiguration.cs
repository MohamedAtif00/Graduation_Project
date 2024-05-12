using Graduation_Project.Domain.Entity.TournamentDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graduation_Project.Infrastructure.DomainConfig.TournamentConfig
{
    public class TournamentEntityTypeConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasConversion(x =>x.value,x =>TournamentId.Create(x));


        }
    }
}
