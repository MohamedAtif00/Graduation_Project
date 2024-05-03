using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Entity.UserDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graduation_Project.Infrastructure.DomainConfig.UserConfig
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x =>x.value,x =>UserId.Create(x));

            builder.Property(x => x.TrainerId).HasConversion(x =>x.value,x =>TrainerId.Create(x));

            //builder.Property(x => x.TimeSessionId).HasConversion(x =>x.value,x =>TimeSessionId.Create(x));

            builder.Property(x => x.HealthCondition).HasConversion(x =>x.Details,x =>HealthCondition.Create(x));

            builder.Property(x =>x.TimeSession).HasConversion(x =>x.ToString(),x =>TimeSession.Parse(x));

            //builder.ComplexProperty(x =>x.HealthCondition);
        }
    }
}
