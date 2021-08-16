using Billdeer.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.DataAccess.Concrete.EntityFramework.Configurations
{
    public class EntityExampleEntityConfig : IEntityTypeConfiguration<EntityExample>
    {
        public void Configure(EntityTypeBuilder<EntityExample> builder)
        {
            builder.HasKey(ee => ee.Id);
            builder.Property(ee => ee.Id).ValueGeneratedOnAdd();

            builder.Property(ee => ee.Name).IsRequired();
            builder.Property(ee => ee.Name).HasMaxLength(100);

            builder
                .HasMany(x => x.ForeignExamples)
                .WithOne(x => x.EntityExample)
                .HasForeignKey(x => x.EntityExampleId);

            builder.ToTable("Examples");
        }
    }
}
