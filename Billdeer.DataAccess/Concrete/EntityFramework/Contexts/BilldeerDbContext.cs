using Billdeer.DataAccess.Concrete.EntityFramework.Configurations;
using Billdeer.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.DataAccess.Concrete.EntityFramework.Contexts
{
    public class BilldeerDbContext : DbContext
    {
        public DbSet<EntityExample> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityExampleEntityConfig());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True; Database=BilldeerDb; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
