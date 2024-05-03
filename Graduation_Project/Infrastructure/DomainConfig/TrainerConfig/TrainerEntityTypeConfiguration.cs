using Graduation_Project.Domain.Entity.TrainerDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graduation_Project.Infrastructure.DomainConfig.TrainerConfig
{
    public class TrainerEntityTypeConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x =>x.value,x =>TrainerId.Create(x));

            //builder.Property()
        }
    }
}
