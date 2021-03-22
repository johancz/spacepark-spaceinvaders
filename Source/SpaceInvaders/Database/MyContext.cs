using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Database
{
    class MyContext : DbContext
    {
        public DbSet<Parking> Parkings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=(local)\SQLExpress;Initial Catalog=SpacePark;Integrated Security=SSPI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>()
                .Property(x => x.StartTime)
                .HasDefaultValueSql("getdate()");
        }
    }
}
