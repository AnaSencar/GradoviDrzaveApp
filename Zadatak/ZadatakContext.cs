using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zadatak
{
    public class ZadatakContext : DbContext
    {
        public ZadatakContext(DbContextOptions<ZadatakContext> options) : base(options)
        {

        }

        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Naselje> Naselja { get; set; }
    }
}
