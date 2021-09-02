using Billdeer.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.DataAccess.Concrete.EntityFramework.Configurations
{
    public class OperationClaimEntityConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.ToTable("OperationClaims");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Alias).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(100);
        }
    }
}
