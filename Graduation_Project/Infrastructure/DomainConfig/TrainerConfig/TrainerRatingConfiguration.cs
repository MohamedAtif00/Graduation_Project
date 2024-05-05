using Graduation_Project.Domain.Entity.TrainerDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graduation_Project.Infrastructure.DomainConfig.TrainerConfig
{
    public class TrainerRatingConfiguration : IEntityTypeConfiguration<TrainerRating>
    {
        public void Configure(EntityTypeBuilder<TrainerRating> builder)
        {
            builder.HasKey(x =>x.Id);

            builder.Property(x => x.Id).HasConversion(x =>x.value,x =>TrainerRatingId.Create(x));
            builder.Property(x => x.trainerId).HasConversion(x =>x.value,x =>TrainerId.Create(x));
        }
    }
}
