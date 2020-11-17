using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;

namespace ApiSW.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Planet> Planets { get; set; }
    }
}