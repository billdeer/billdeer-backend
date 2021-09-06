using Billdeer.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billdeer.DataAccess.Concrete.EntityFramework.Configurations
{
    public class UserOperationClaimEntityConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("UserOperationClaims");

            builder.HasKey( uo => uo.Id);

            builder
                .HasOne(uo => uo.User)
                .WithMany(u => u.UserOperationClaims)
                .HasForeignKey(uoc => uoc.UserId);

            builder
                .HasOne(uo => uo.OperationClaim)
                .WithMany(o => o.UserOperationClaims)
                .HasForeignKey(uoc => uoc.OperationClaimId);
        }
    }
}
