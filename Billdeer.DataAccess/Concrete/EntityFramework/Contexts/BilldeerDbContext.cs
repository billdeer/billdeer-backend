using Billdeer.Core.Entities.Concrete;
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=Billdeer;Username=postgres;Password=12345;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BilldeerDbContext).Assembly);
        }
        public DbSet<EntityExample> Examples { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
        //{
        //  "email": "mehmetkaya@billdeer.com",
        //  "password": "123Kaya123#",
        //  "firstName": "Mehmet",
        //  "lastName": "Kaya",
        //  "username": "mehmetkaya"
        //}

    }
}
