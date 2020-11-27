using System;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApiSW
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Planet> Planets { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=swdb;Trusted_Connection=True;");
        }
    }
}